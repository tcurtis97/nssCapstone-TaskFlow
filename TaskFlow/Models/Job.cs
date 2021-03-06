using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Models
{
    public class Job
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int AddressId { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CompletionDate { get; set; }


        public DateTime CreateDate { get; set; }

        public Customer Customer { get; set; }

        public CustomerAddress Address { get; set; }

        public WorkDay WorkDay { get; set; }

        public List<Note> notes;

        public UserProfile userProfile { get; set; }
    }
}
