using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Helpers;
using EduHomeApp.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduHomeApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddIdentity<EduHomeUser, IdentityRole>(identityoptions =>
            {
                //Password Setting
                identityoptions.Password.RequiredLength = 8;
                identityoptions.Password.RequireDigit = true;
                identityoptions.Password.RequireLowercase = true;
                identityoptions.Password.RequireUppercase = true;
                identityoptions.Password.RequireNonAlphanumeric = true;

                //Lockout Setting
                identityoptions.Lockout.MaxFailedAccessAttempts = 3;
                identityoptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                identityoptions.Lockout.AllowedForNewUsers = true;

                //User Setting
                identityoptions.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<EduHomeDbContext>()
           .AddDefaultTokenProviders();

            services.AddDbContext<EduHomeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                 );
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
