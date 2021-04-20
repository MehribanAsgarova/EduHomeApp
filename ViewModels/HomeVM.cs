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
    }
}
