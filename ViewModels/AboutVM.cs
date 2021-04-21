using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;

namespace EduHomeApp.ViewModels
{
    public class AboutVM
    {
        public IEnumerable<About> Abouts { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
    }
}
