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

        public List<Note> Search(string criterion, bool sortDescending)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql =
                        @"SELECT c.Id, c.[Name], c.PhoneNumber
                    FROM Note c 
                       
                    WHERE c.[Name] LIKE @Criterion OR c.PhoneNumber LIKE @Criterion
                    ORDER BY  Name";



                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
                    var reader = cmd.ExecuteReader();

                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        notes.Add(new Note()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                        });
                    }

                    reader.Close();

                    return notes;
                }
            }
        }


        public Note GetNoteByIdWithAddressWithJob(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT c.[Name], c.PhoneNumber,

                                 a.Id AS AddressId, a.NoteId, a.Address,

                                 j.Id AS JobId, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, 
                                 ISNULL(j.CompletionDate, '') as CompletionDate, j.CreateDate, j.NoteId
                            
                            FROM Note c
                            LEFT JOIN Address a ON a.NoteId = c.Id
                            LEFT JOIN Job j ON j.NoteId = c.Id
                           WHERE c.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Note note = null;
                    if (reader.Read())
                    {
                        note = new Note()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            Addresses = new List<NoteAddress>(),
                            Jobs = new List<Job>()
                        };

                        if (DbUtils.IsNotDbNull(reader, "AddressId"))
                        {
                            note.Addresses.Add(new NoteAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                Address = DbUtils.GetString(reader, "Address"),
                                NoteId = id,

                            });
                        }

                        if (DbUtils.IsNotDbNull(reader, "JobId"))
                        {
                            note.Jobs.Add(new Job()
                            {
                                Id = DbUtils.GetInt(reader, "JobId"),
                                Descritpion = DbUtils.GetString(reader, "Description"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                                CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                                NoteId = id,
                            });
                        }

                    }
                    reader.Close();

                    return note;

                }
            }
        }
















    }
}
