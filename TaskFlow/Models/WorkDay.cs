using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Models
{
    public class WorkDay
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }

        public int JobId { get; set; }
    }
}
