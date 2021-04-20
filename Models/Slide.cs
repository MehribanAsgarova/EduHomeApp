using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeApp.Models
{
    public class Slide
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
