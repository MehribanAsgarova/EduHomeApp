using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EduHomeApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public virtual ICollection<BlogComment> BlogComment { get; set; }

    }
}
