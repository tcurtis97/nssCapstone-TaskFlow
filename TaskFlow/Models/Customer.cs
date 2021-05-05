using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        
        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public List<CustomerAddress> Addresses { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
