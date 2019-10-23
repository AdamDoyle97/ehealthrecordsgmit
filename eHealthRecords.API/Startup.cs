using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eHealthRecords.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eHealthRecords.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //putting it in services injects it straight into the project startup
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); //installed sqlite package 3.0.0 and used dotnet restore``
            services.AddControllers();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           /*  else
            {
                //The default HSTS value is 30 days. Change this for production scenarios, https://aka.ms/aspnetcore-hsts.
               // app.UseHsts(); //turn this off as we dont want it to go into production, yet
            }*/
           // app.UseHttpsRedirection(); //turn this off as this will redirect to https 
           app.UseCors(x => x.AllowAnyOrigin().AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader());
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}