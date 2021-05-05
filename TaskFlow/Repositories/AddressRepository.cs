using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public AddressRepository(IConfiguration configuration) : base(configuration) { }
        public List<CustomerAddress> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  Id, CustomerId, Address
                          FROM  Address
                      ORDER BY  Name";

                    var reader = cmd.ExecuteReader();

                    var addresss = new List<CustomerAddress>();
                    while (reader.Read())
                    {
                        addresss.Add(new CustomerAddress()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            Address = DbUtils.GetString(reader, "Address"),

                        });
                    }

                    reader.Close();

                    return addresss;
                }
            }
        }





        public void Add(CustomerAddress address)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Address (CustomerId, Address)
                                        OUTPUT INSERTED.ID
                                        VALUES (@CustomerId, @Address)";
                    DbUtils.AddParameter(cmd, "@CustomerId", address.CustomerId);
                    DbUtils.AddParameter(cmd, "@Address", address.Address);


                    address.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int addressId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Address WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", addressId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(CustomerAddress address)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Address
                           SET CustomerId = @CustomerId,
                                Address = @Address
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@CustomerId", address.CustomerId);
                    DbUtils.AddParameter(cmd, "@Address", address.Address);
                    DbUtils.AddParameter(cmd, "@Id", address.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }







    }
}
