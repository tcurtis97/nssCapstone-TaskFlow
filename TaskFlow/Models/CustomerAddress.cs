using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        
        public int CustomerId { get; set; }

        public string Address { get; set; }
    
    }
}
