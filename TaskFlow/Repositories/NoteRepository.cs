using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class NoteRepository : BaseRepository
    {
        public NoteRepository(IConfiguration configuration) : base(configuration) { }
        public List<Note> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  Id, UserProfileId, JobId, Date, NoteText
                          FROM  Note";

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(new Note()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            Date = DbUtils.GetDateTime(reader, "Date"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),

                        });
                    }

                    reader.Close();

                    return notes;
                }
            }
        }

        public Note GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT  UserProfileId, JobId, Date, NoteText
                            FROM Note 
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Note note = null;
                    if (reader.Read())
                    {
                        note = new Note()
                        {
                            Id = id,
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            Date = DbUtils.GetDateTime(reader, "Date"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            

                        };
                    }

                    reader.Close();

                    return note;
                }
            }
        }



        public void Add(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (UserProfileId, JobId, Date, NoteText)
                                        OUTPUT INSERTED.ID
                                        VALUES (@UserProfileId, @JobId, @Date, @NoteText)";
                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", note.JobId);
                    DbUtils.AddParameter(cmd, "@Date", note.Date);
                    DbUtils.AddParameter(cmd, "@NoteText", note.NoteText);


                    note.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int noteId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Note WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", noteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Note note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Note
                           SET UserProfileId = @UserProfileId,
                                JobId = @JobId
                                Date = @Date
                                NoteText = @NoteText
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", note.JobId);
                    DbUtils.AddParameter(cmd, "@Date", note.Date);
                    DbUtils.AddParameter(cmd, "@NoteText", note.NoteText);

                    cmd.ExecuteNonQuery();
                }
            }
        }

      


       
















    }
}
