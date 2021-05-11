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

        public DateTime CreateDate { get; set; }

        public string NoteText { get; set; }

        public UserProfile userProfile { get; set; }
        public decimal TimeOnJob { get; set; }
    }
}
