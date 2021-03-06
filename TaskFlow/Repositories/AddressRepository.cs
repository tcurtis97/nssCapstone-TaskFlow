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
                     ";

                    var reader = cmd.ExecuteReader();

                    var addresses = new List<CustomerAddress>();
                    while (reader.Read())
                    {
                        addresses.Add(new CustomerAddress()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            Address = DbUtils.GetString(reader, "Address"),

                        });
                    }

                    reader.Close();

                    return addresses;
                }
            }
        }


        public CustomerAddress GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, CustomerId, Address
                          FROM  Address
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    CustomerAddress address = null;
                    if (reader.Read())
                    {
                        address = new CustomerAddress()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            Address = DbUtils.GetString(reader, "Address"),

                        };
                    }

                    reader.Close();

                    return address;
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

        public List<CustomerAddress> GetAllAddressesByCustomerId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  a.Id, a.CustomerId, a.Address
                                        

                                        FROM Address a
                                        LEFT JOIN Customer c on c.id = a.customerId
                                        WHERE a.CustomerId = @id
                                        ";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    var addresses = new List<CustomerAddress>();
                    while (reader.Read())
                    {
                        addresses.Add(new CustomerAddress()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                            Address = DbUtils.GetString(reader, "Address"),
                        });
                    }

                    reader.Close();

                    return addresses;
                }
            }
        }





    }
}
