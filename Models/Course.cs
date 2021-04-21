using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }

        public virtual CourseFeature CourseFeature { get; set; }
        public int CourseFeatureId { get; set; }
    }
}
