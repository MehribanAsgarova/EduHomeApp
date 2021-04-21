using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public virtual ICollection<SpeakerEvent> SpeakerEvent { get; set; }
    }
}
