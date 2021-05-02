using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.ViewModels
{
    public class SubscribeVM
    {
        public int Id { get; set; }
        [Required]
        public int Email { get; set; }
    }
}
