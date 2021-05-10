using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public class WorkDayRepository : BaseRepository, IWorkDayRepository
    {
        public WorkDayRepository(IConfiguration configuration) : base(configuration) { }

        public void AddWorkDay(WorkDay workDay)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO WorkDay(UserProfileId, JobId)
                        OUTPUT INSERTED.ID
                        VALUES (@userProfileId, @jobId)";

                    cmd.Parameters.AddWithValue("@userProfileId", workDay.UserProfileId);
                    cmd.Parameters.AddWithValue("@jobId", workDay.JobId);

                    int id = (int)cmd.ExecuteScalar();

                    workDay.Id = id;
                }
            }
        }

        public void DeleteWorkDay(int workDayId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM WorkDay
                        WHERE Id = @workDayId";

                    cmd.Parameters.AddWithValue("@workDayId", workDayId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateWorkDay(WorkDay workDay)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE WorkDay
                        set
                            UserProfileId = @userProfileId,
                            JobId = @jobId
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@userProfileId", workDay.UserProfileId);
                    cmd.Parameters.AddWithValue("@jobId", workDay.JobId);
                    cmd.Parameters.AddWithValue("@id", workDay.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void WorkDayExist(WorkDay workDay)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT DISTINCT COUNT(Id) AS Count, UserProfileId, JobId
                        FROM WorkDay
                        WHERE UserProfileId = @userProfileId AND JobId = @jobId
                        GROUP BY UserProfileId, JobId";

                    cmd.Parameters.AddWithValue("@userProfileId", workDay.UserProfileId);
                    cmd.Parameters.AddWithValue("@jobId", workDay.JobId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }