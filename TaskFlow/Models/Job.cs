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

        public string Descritpion { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ComepletedDate { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
