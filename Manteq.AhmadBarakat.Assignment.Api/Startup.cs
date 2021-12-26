using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Api.Persistense.Repositories.Interfaces;
using Api.Persistense.Repositories.EF;
using Api.Context.Interfaces;
using Api.Persistense.UnitOfWork;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Api.MiddleWares;
using Serilog;

namespace Manteq.AhmadBarakat.Assignment.Api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            #region EF db context
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IDbContext, ApplicationDbContext>();
            #endregion
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IPatientRepository, PatientRepository>();
            #endregion
            #region Unit of Work
            services.AddTransient<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            #endregion
            #region Services
            services.AddTransient<IPatientService, PatientService>();
            #endregion

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Patient API",
                    Version = "v1",
                    Description = "This Api provides CRUD operations for patient data.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ahmad Mulhem Barakat",
                        Email = "molham225@gmail.com",
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient API V1");

                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
