using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using German.Core.Interfaces;
using German.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace German.Web.MVC
{
	public class Startup

	{
		public Startup(IConfiguration configuration)
		{
            this.configuration = configuration;
		}

		public IConfiguration configuration { get; }


        //ets caolled by runtime, use to add services to container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IAppDbContext, AppDbContext>();
          

        }

        //run  runtime to configure http request pipeline

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

