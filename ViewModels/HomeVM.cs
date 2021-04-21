using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;

namespace EduHomeApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
        public IEnumerable<InfoBoard> InfoBoards { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }

    }
}
