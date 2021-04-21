using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class CourseFeature
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int StudentCount { get; set; }
        public string Assesment { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
