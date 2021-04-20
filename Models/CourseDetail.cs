using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public string AboutCourse { get; set; }
        public string AboutCourseDescription { get; set; }
        public string HowToApply { get; set; }
        public string HowToApplyDescription { get; set; }
        public string Certification { get; set; }
        public string CertificationDescription { get; set; }
        public ICollection<LeaveReply> leaveReplies { get; set; }
    }
}
