using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Utils;

namespace TaskFlow.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration) { }
        public List<Customer> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  Id, [Name], PhoneNumber
                          FROM  Customer
                      ORDER BY  Name";

                    var reader = cmd.ExecuteReader();

                    var customers = new List<Customer>();
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),

                        });
                    }

                    reader.Close();

                    return customers;
                }
            }
        }

        public Customer GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT c.[Name], c.PhoneNumber
                            FROM Customer c
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Customer customer = null;
                    if (reader.Read())
                    {
                        customer = new Customer()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),

                        };
                    }

                    reader.Close();

                    return customer;
                }
            }
        }



        public void Add(Customer customer)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Customer ([Name], PhoneNumber)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @PhoneNumber)";
                    DbUtils.AddParameter(cmd, "@Name", customer.Name);
                    DbUtils.AddParameter(cmd, "@PhoneNumber", customer.PhoneNumber);


                    customer.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int customerId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Customer WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", customerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Customer customer)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Customer
                           SET [Name] = @Name,
                                PhoneNumber = @PhoneNumber
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", customer.Name);
                    DbUtils.AddParameter(cmd, "@Id", customer.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }








    }
}
