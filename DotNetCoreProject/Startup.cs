using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreProject.Database;
using DotNetCoreProject.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCoreProject
{
    public class Startup
    {
        private IConfiguration _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{ 
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute(); //this is default route"{controller=Home}/{action=Index}/{id?}"
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "route-one",
                //    template: "Index",
                //    defaults: new { controller = "Home", action = "Index" }
                //    );
                //routes.MapRoute(
                //    name: "route-two",
                //    template: "Details/{id}",
                //    defaults: new { controller = "Home", action = "Details" });

                //routes.MapRoute(
                //    name: "default",
                //    template: "LearnCode/{controller=Home}/{action=Index}/{id?}"
                //    );
                routes.MapRoute(
                    "default",
                    "LearnCode/{controller=Home}/{action=Index}/{id?}"
                   );
            });

            //app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Some error processing request!!");
            //    await context.Response
            //    .WriteAsync("Hello world!!");
            //});
        }
    }
}
