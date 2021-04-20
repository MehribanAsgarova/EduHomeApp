using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class CourseWeOffer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual CourseDetail CourseId { get; set; }
    }
}
