using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Models
{
    public class WorkRecord
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }

        public int JobId { get; set; }

        public DateTime Date { get; set; }

        public string WorkText { get; set; }

        public int TimeOnJob { get; set; }
    }
}
