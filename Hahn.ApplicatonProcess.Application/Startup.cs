using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.Application.ActionFilters;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Classes;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Services.Classes;
using Hahn.ApplicatonProcess.December2020.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Shared.Validators;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application
{
    /// <summary>
    /// commit 3
    /// </summary>
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

            services.AddControllers();

            ConfigureInjection(services);
          

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc(options=>options.Filters.Add(new ValidationFilter()))
            .AddFluentValidation(options=>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });


            //Database Context
            services.AddDbContext<HahnDBContext>(options =>
            options.UseInMemoryDatabase("HahnDB"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.Application", Version = "v1" });
                
                
            });

            services.AddLogging(
    builder =>
    {
        builder//.AddFilter("Microsoft", LogLevel.Information)
               .AddFilter("System", LogLevel.Error)
               .AddConsole();
    });
        }

        private static void ConfigureInjection(IServiceCollection services)
        {
            services.AddScoped<IApplicantService, ApplicantService>();

            services.AddScoped<IApplicantRepository, ApplicantRepository>();

           
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
              
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.Application v1"));
            }


            var context = app.ApplicationServices.GetService<HahnDBContext>();
            AddTestData(context);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(HahnDBContext context)
        {
            var testApplicant1 = new Applicant
            {
                Id = 1,

                Name = "Mohammed",
                FamilyName = "Thabet",
                Address="Cairo",
                Age=30,
                CountryOfOrigin="Egypt",
                CreationDateTime=DateTime.Now,
                ModificationDateTime=DateTime.Now,
                EmailAddress="thabet084@hotmail.com",
                Hired=true

            };

            context.Applicants.Add(testApplicant1);

            var testApplicant2 = new Applicant
            {
                Id = 2,

                Name = "Yassin",
                FamilyName = "Thabet",
                Address = "Cairo",
                Age = 30,
                CountryOfOrigin = "Egypt",
                CreationDateTime = DateTime.Now,
                ModificationDateTime = DateTime.Now,
                EmailAddress = "yassin@hotmail.com",
                Hired = true

            };

            context.Applicants.Add(testApplicant2);

            context.SaveChanges();
        }

    }
}
