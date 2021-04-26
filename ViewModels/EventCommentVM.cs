using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.ViewModels
{
    public class EventCommentVM
    {
        public int id { get; set; }
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}
