using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.DAL
{
    public class EduHomeDbContext:IdentityDbContext<EduHomeUser>
    {
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options):base(options){}
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Blog> Blogs{ get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SpeakerEvent> SpeakerEvents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<InfoBoard> InfoBoards { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<ContactComment> ContactComments { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }


    }
}
