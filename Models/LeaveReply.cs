using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class LeaveReply
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Mesage { get; set; }
        public int CourseDetailId { get; set; }
        public CourseDetail courseDetail { get; set; }

    }
}
