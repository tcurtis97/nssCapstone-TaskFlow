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
                    DbUtils.AddParameter(cmd, "@PhoneNumber", customer.PhoneNumber);
                    DbUtils.AddParameter(cmd, "@Id", customer.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Customer> Search(string criterion, bool sortDescending)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql =
                        @"SELECT c.Id, c.[Name], c.PhoneNumber
                    FROM Customer c 
                       
                    WHERE c.[Name] LIKE @Criterion OR c.PhoneNumber LIKE @Criterion
                    ORDER BY  Name";



                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
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


        public Customer GetCustomerByIdWithAddressWithJob(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT c.[Name], c.PhoneNumber,

                                 a.Id AS AddressId, a.CustomerId, a.Address,

                                 j.Id AS JobId, j.Description, ISNULL(j.ImageUrl, '') as ImageUrl, 
                                 ISNULL(j.CompletionDate, '') as CompletionDate, j.CreateDate, j.CustomerId
                            
                            FROM Customer c
                            LEFT JOIN Address a ON a.CustomerId = c.Id
                            LEFT JOIN Job j ON j.CustomerId = c.Id
                           WHERE c.Id = @Id";

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
                            Addresses = new List<CustomerAddress>(),
                            Jobs = new List<Job>()
                        };

                        if (DbUtils.IsNotDbNull(reader, "AddressId"))
                        {
                            customer.Addresses.Add(new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                Address = DbUtils.GetString(reader, "Address"),
                                CustomerId = id,

                            });
                        }

                            if (DbUtils.IsNotDbNull(reader, "JobId"))
                            {
                                customer.Jobs.Add(new Job()
                                {
                                    Id = DbUtils.GetInt(reader, "JobId"),
                                    Descritpion = DbUtils.GetString(reader, "Description"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                    CompletionDate = DbUtils.GetDateTime(reader, "CompletionDate"),
                                    CreateDate = DbUtils.GetDateTime(reader, "CreateDate"),
                                    CustomerId = id,
                                });
                            }

                    }
                            reader.Close();

                            return customer;

                }
            }
        }




        public List<Customer> getCustomersWithAddress()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 SELECT  c.Id AS CustomerId, c.[Name], c.PhoneNumber,

                                 a.Id AS AddressId, a.CustomerId, a.Address

                          FROM  Customer c
                           JOIN Address a ON a.CustomerId = c.Id
                      ";

                    var reader = cmd.ExecuteReader();

                    var customers = new List<Customer>();
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            Id = DbUtils.GetInt(reader, "CustomerId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            PhoneNumber = DbUtils.GetString(reader, "PhoneNumber"),
                            Address = new CustomerAddress()
                            {
                                Id = DbUtils.GetInt(reader, "AddressId"),
                                CustomerId = DbUtils.GetInt(reader, "CustomerId"),
                                Address = DbUtils.GetString(reader, "Address"),
                            },
                        });
                    }

                    reader.Close();

                    return customers;
                }
            }
        }












    }

}

        




    

