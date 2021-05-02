using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        [Required]
        public int Email { get; set; }
    }
}
