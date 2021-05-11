using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class WorkRecordRepository : BaseRepository, IWorkRecordRepository
    {
        public WorkRecordRepository(IConfiguration configuration) : base(configuration) { }
        public List<WorkRecord> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  w.Id, w.UserProfileId, w.JobId, w.CreateDate, w.NoteText, w.TimeOnJob, u.Id, u.DisplayName
                          FROM  WorkRecord w
                            LEFT JOIN UserProfile u ON w.UserProfileId = u.Id";

                    var reader = cmd.ExecuteReader();

                    var workRecords = new List<WorkRecord>();
                    while (reader.Read())
                    {
                        workRecords.Add(new WorkRecord()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            TimeOnJob = DbUtils.GetDecimal(reader, "TimeOnJob"),
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            },

                        });
                    }

                    reader.Close();

                    return workRecords;
                }
            }
        }

        public WorkRecord GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT  UserProfileId, JobId, CreateDate, NoteText, TimeOnJob
                            FROM WorkRecord 
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    WorkRecord workRecord = null;
                    if (reader.Read())
                    {
                        workRecord = new WorkRecord()
                        {
                            Id = id,
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            TimeOnJob = DbUtils.GetDecimal(reader, "TimeOnJob"),


                        };
                    }

                    reader.Close();

                    return workRecord;
                }
            }
        }



        public void Add(WorkRecord workRecord)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO WorkRecord (UserProfileId, JobId, CreateDate, NoteText, TimeOnJob)
                                        OUTPUT INSERTED.ID
                                        VALUES (@UserProfileId, @JobId, @CreateDate, @NoteText, @TimeOnJob)";
                    DbUtils.AddParameter(cmd, "@UserProfileId", workRecord.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", workRecord.JobId);
                    DbUtils.AddParameter(cmd, "@CreateDate", workRecord.CreateDate);
                    DbUtils.AddParameter(cmd, "@NoteText", workRecord.NoteText);
                    DbUtils.AddParameter(cmd, "@TimeOnJob", workRecord.TimeOnJob);


                    workRecord.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int workRecordId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM WorkRecord WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", workRecordId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(WorkRecord workRecord)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE WorkRecord
                           SET UserProfileId = @UserProfileId,
                                JobId = @JobId
                                CreateDate = @CreateDate
                                NoteText = @NoteText
                                TimeOnJob = @TimeOnJob
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@UserProfileId", workRecord.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", workRecord.JobId);
                    DbUtils.AddParameter(cmd, "@CreateDate", workRecord.CreateDate);
                    DbUtils.AddParameter(cmd, "@NoteText", workRecord.NoteText);
                    DbUtils.AddParameter(cmd, "@TimeOnJob", workRecord.TimeOnJob);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<WorkRecord> GetAllWorkRecordsByJobId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT   w.Id, w.UserProfileId, w.JobId, w.CreateDate, w.NoteText, w.TimeOnJob, u.Id, u.DisplayName
                                                    FROM  WorkRecord w
                                                    LEFT JOIN UserProfile u ON w.UserProfileId = u.Id
                                                    WHERE w.JobId = @id
                                        ";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    var workRecords = new List<WorkRecord>();
                    while (reader.Read())
                    {
                        workRecords.Add(new WorkRecord()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            TimeOnJob = DbUtils.GetDecimal(reader, "TimeOnJob"),
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            },
                        });
                    }

                    reader.Close();

                    return workRecords;
                }
            }
        }


















    }
}

