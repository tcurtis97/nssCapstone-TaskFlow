using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(IConfiguration configuration) : base(configuration) { }
        public List<Job> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  j.Id, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, ISNULL(j.CompletionDate, '') as CompletionDate,
                           j.CreateDate, j.CustomerId, j.AddressId,

                            c.Id AS CustomerId, c.[Name], c.PhoneNumber,


                                 a.Id AS AddressId, a.CustomerId, a.Address,

                         
                        ISNULL( wd.UserProfileId, 0) as UserProfileId, ISNULL(wd.Id, 0) as WorkDayId, ISNULL(wd.JobId, 0) AS JobId,

                         ISNULL( u.Id, 0) as UserProfileId, ISNULL(u.DisplayName, '') as DisplayName


                          FROM  Job j
                    LEFT JOIN WorkDay wd on j.Id = wd.JobId
                    LEFT JOIN Address a ON j.AddressId = a.Id
                     LEFT JOIN Customer c ON j.CustomerId = c.Id
                    LEFT JOIN UserProfile u ON wd.UserProfileId = u.Id
                    ";

                    var reader = cmd.ExecuteReader();

                    var jobs = new List<Job>();
                    while (reader.Read())
                    {
                        jobs.Add(new Job()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            Customer = new Customer()
                            {
                                Id = DbUtils.GetInt(reader, "CustomerId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            },
                            Address = new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                Address = DbUtils.GetString(reader, "Address"),
                            },
                            WorkDay = new WorkDay()
                            {
                                Id = DbUtils.GetInt(reader, "WorkDayId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                JobId = DbUtils.GetInt(reader, "JobId"),
                            },
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            },

                        });
                    }

                    reader.Close();

                    return jobs;
                }
            }
        }

        public Job GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT  j.Id, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, ISNULL(j.CompletionDate, '') as CompletionDate,
                           j.CreateDate, j.CustomerId, j.AddressId
                          FROM  Job j
                    WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Job job = null;
                    if (reader.Read())
                    {
                        job = new Job()
                        {
                            Id = id,
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),

                        };
                    }

                    reader.Close();

                    return job;
                }
            }
        }



        public void Add(Job job)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Job (Description, ImageUrl, CreateDate, CustomerId, AddressId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Description, @ImageUrl, @CreateDate, @CustomerId, @AddressId)";
                    DbUtils.AddParameter(cmd, "@Description", job.Description);
                    DbUtils.AddParameter(cmd, "@ImageUrl", job.ImageUrl);
        
                    DbUtils.AddParameter(cmd, "@CreateDate", job.CreateDate);
                    DbUtils.AddParameter(cmd, "@CustomerId", job.CustomerId);
                    DbUtils.AddParameter(cmd, "@AddressId", job.AddressId);



                    job.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int jobId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Job WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", jobId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Job job)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Job
                           SET Description = @Description,
                                ImageUrl = @ImageUrl
                                CompletionDate = @CompletionDate
                                CreateDate = @CreateDate
                                CustomerId = @CustomerId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Description", job.Description);
                    DbUtils.AddParameter(cmd, "@ImageUrl", job.ImageUrl);
                    DbUtils.AddParameter(cmd, "@CompletionDate", job.CompletionDate);
                    DbUtils.AddParameter(cmd, "@CreateDate", job.CreateDate);
                    DbUtils.AddParameter(cmd, "@CustomerId", job.CustomerId);
                    DbUtils.AddParameter(cmd, "@AddressId", job.AddressId);
                    DbUtils.AddParameter(cmd, "@Id", job.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public Job GetJobByIdWithDetails(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT 
                                 j.Id AS JobId, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, 
                                 ISNULL(j.CompletionDate, '') as CompletionDate, j.CreateDate, j.CustomerId,
                                    

                                    c.Id AS CustomerId, c.[Name], c.PhoneNumber,

                                 

                                    a.Id AS AddressId, a.CustomerId, a.Address,
                                    
                                    n.Id AS NoteId, n.UserProfileId, n.JobId, n.CreateDate, n.NoteText
                            
                            FROM Job j
                            LEFT JOIN Address a ON j.AddressId = a.Id
                            LEFT JOIN Customer c ON j.CustomerId = c.Id
                             LEFT JOIN Note n ON j.Id = n.JobId
                           WHERE j.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Job job = null;
                    if (reader.Read())
                    {
                        job = new Job()
                        {
                            Id = id,
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                          
                            Address = new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                                Address = DbUtils.GetString(reader, "Address"),
                            },
                            Customer = new Customer() 
                            {
                                Id = DbUtils.GetInt(reader, "CustomerId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            },
                        };

                      



                    }
                    reader.Close();

                    return job;

                }
            }
        }

        public List<Job> GetAllJobsByCustomerId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT   j.Id, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, 
                                 ISNULL(j.CompletionDate, '') as CompletionDate, j.CreateDate, j.CustomerId
                                        

                                        FROM Job j
                                        LEFT JOIN Customer c on c.id = j.CustomerId
                                        WHERE j.CustomerId = @id
                                        ";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    var jobs = new List<Job>();
                    while (reader.Read())
                    {
                        jobs.Add(new Job()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                        });
                    }

                    reader.Close();

                    return jobs;
                }
            }
        }

        public void ComepleteJob(int id, DateTime complete)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Job
                           SET  CompletionDate= @complete,
                               
                         WHERE Id = @Id";

                    
                    DbUtils.AddParameter(cmd, "@Id", id);
                    DbUtils.AddParameter(cmd, "@complete", complete);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Job> GetAllUncompleteJobs()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT   j.Id, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, ISNULL(j.CompletionDate, '') as CompletionDate,
                           j.CreateDate, j.CustomerId, j.AddressId,

                            c.Id AS CustomerId, c.[Name], c.PhoneNumber,


                                 a.Id AS AddressId, a.CustomerId, a.Address



                          FROM  Job j
                    LEFT JOIN Address a ON j.AddressId = a.Id
                     LEFT JOIN Customer c ON j.CustomerId = c.Id
                            WHERE j.CompletionDate IS NULL
                    ";

                    var reader = cmd.ExecuteReader();

                    var jobs = new List<Job>();
                    while (reader.Read())
                    {
                        jobs.Add(new Job()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            AddressId = DbUtils.GetInt(reader, "AddressId"),
                            Customer = new Customer()
                            {
                                Id = DbUtils.GetInt(reader, "CustomerId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            },
                            Address = new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                Address = DbUtils.GetString(reader, "Address"),
                            },

                        });
                    }

                    reader.Close();

                    return jobs;
                }
            }
        }

        public List<Job> GetJobsByWorkDayUser(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  j.Id, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, ISNULL(j.CompletionDate, '') as CompletionDate,
                           j.CreateDate, j.CustomerId, j.AddressId, wd.UserProfileId, wd.Id AS WorkDayId, wd.JobId,

                                       c.Id AS CustomerId, c.[Name], c.PhoneNumber,


                                 a.Id AS AddressId, a.CustomerId, a.Address
      
                        FROM    Job j
                                LEFT JOIN WorkDay wd on j.Id = wd.JobId
                                LEFT JOIN Address a ON j.AddressId = a.Id
                                LEFT JOIN Customer c ON j.CustomerId = c.Id
                        WHERE wd.UserProfileId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    List<Job> jobs = new List<Job>();

                    while (reader.Read())
                    {
                        jobs.Add(new Job()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            AddressId = DbUtils.GetInt(reader, "AddressId"),
                            WorkDay = new WorkDay()
                            {
                                Id = DbUtils.GetInt(reader, "WorkDayId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                JobId = DbUtils.GetInt(reader, "JobId"),
                            },
                            Address = new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                Address = DbUtils.GetString(reader, "Address"),
                            },
                            Customer = new Customer()
                            {
                                Id = DbUtils.GetInt(reader, "CustomerId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            },

                        });

                    }

                    reader.Close();
                    return jobs;
                }
            }
        }




    }

}

