using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EduHomeApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public virtual ICollection<SpeakerEvent> SpeakerEvent { get; set; }
        
     }
}
