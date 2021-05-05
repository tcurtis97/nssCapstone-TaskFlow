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
                 SELECT  j.Id, j.Description, ISNULL(j.ImageUrl, 'Missing') as ImageUrl, ISNULL(j.CompletionDate, '') as CompletionDate, j.CreateDate, j.CustomerId
                          FROM  Job j";

                    var reader = cmd.ExecuteReader();

                    var jobs = new List<Job>();
                    while (reader.Read())
                    {
                        jobs.Add(new Job()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Descritpion = DbUtils.GetString(reader, "Description"),
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

        public Job GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT  Id, Description, ImageUrl, CompletionDate, CreateDate, CustomerId
                          FROM  Job
                    WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Job job = null;
                    if (reader.Read())
                    {
                        job = new Job()
                        {
                            Id = id,
                            Descritpion = DbUtils.GetString(reader, "Description"),
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
                    cmd.CommandText = @"INSERT INTO Job (Description, ImageUrl, CompletionDate, CreateDate, CustomerId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Description, @ImageUrl, @CompletionDate, @CreateDate, @CustomerId)";
                    DbUtils.AddParameter(cmd, "@Description", job.Descritpion);
                    DbUtils.AddParameter(cmd, "@ImageUrl", job.ImageUrl);
                    DbUtils.AddParameter(cmd, "@CompletionDate", job.CompletionDate);
                    DbUtils.AddParameter(cmd, "@CreateDate", job.CreateDate);
                    DbUtils.AddParameter(cmd, "@CustomerId", job.CustomerId);


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

                    DbUtils.AddParameter(cmd, "@Description", job.Descritpion);
                    DbUtils.AddParameter(cmd, "@ImageUrl", job.ImageUrl);
                    DbUtils.AddParameter(cmd, "@CompletionDate", job.CompletionDate);
                    DbUtils.AddParameter(cmd, "@CreateDate", job.CreateDate);
                    DbUtils.AddParameter(cmd, "@CustomerId", job.CustomerId);
                    DbUtils.AddParameter(cmd, "@Id", job.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }








    }

}

