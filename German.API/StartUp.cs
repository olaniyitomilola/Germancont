using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using German.Core.Interfaces;
using German.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;


namespace German.API
{
    public class StartUp

    {
        public StartUp(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration configuration { get; }


        //ets caolled by runtime, use to add services to container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "German LMS", Version = "v1" });
            });
           

        }

        //run  runtime to configure http request pipeline

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
         

            app.UseHttpsRedirection();

            app.UseRouting();

         //   app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

