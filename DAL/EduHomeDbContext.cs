using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.DAL
{
    public class EduHomeDbContext:DbContext
    {
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options):base(options){}
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Notice> Notices { get; set; }
    }
}
