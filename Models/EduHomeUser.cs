using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EduHomeApp.Models
{
    public class EduHomeUser:IdentityUser
    {
        [Required, StringLength(200)]
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
