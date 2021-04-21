using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;

namespace EduHomeApp.ViewModels
{
    public class TeacherVM
    {
       
        public Teacher Teacher { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
