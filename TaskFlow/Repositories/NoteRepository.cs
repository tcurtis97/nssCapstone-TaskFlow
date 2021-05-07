using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
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
                 SELECT  n.Id, n.UserProfileId, n.JobId, n.CreateDate, n.NoteText, u.Id, u.DisplayName
                          FROM  Note n
                            LEFT JOIN UserProfile u ON n.UserProfileId = u.Id";

                    var reader = cmd.ExecuteReader();

                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(new Note()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            },

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
                          SELECT  UserProfileId, JobId, CreateDate, NoteText
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
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
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
                    cmd.CommandText = @"INSERT INTO Note (UserProfileId, JobId, CreateDate, NoteText)
                                        OUTPUT INSERTED.ID
                                        VALUES (@UserProfileId, @JobId, @CreateDate, @NoteText)";
                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", note.JobId);
                    DbUtils.AddParameter(cmd, "@CreateDate", note.CreateDate);
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
                                CreateDate = @CreateDate
                                NoteText = @NoteText
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@JobId", note.JobId);
                    DbUtils.AddParameter(cmd, "@CreateDate", note.CreateDate);
                    DbUtils.AddParameter(cmd, "@NoteText", note.NoteText);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Note> GetAllNotesByJobId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT   n.Id, n.UserProfileId, n.JobId, n.CreateDate, n.NoteText, u.Id, u.DisplayName
                                                    FROM  Note n
                                                    LEFT JOIN UserProfile u ON n.UserProfileId = u.Id
                                                    WHERE n.JobId = @id
                                        ";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(new Note()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            JobId = DbUtils.GetInt(reader, "JobId"),
                            CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                            NoteText = DbUtils.GetString(reader, "NoteText"),
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            },
                        });
                    }

                    reader.Close();

                    return notes;
                }
            }
        }


















    }
}
