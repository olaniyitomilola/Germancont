using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using German.Core.Interfaces;
using German.Application.Services;
using German.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using German.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
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
            services.AddControllers()
                .AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            services.AddCors(opt => {
                opt.AddPolicy("justLocalHost5500", (CorsPolicyBuilder obj) => obj.WithOrigins("https://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod());
                opt.AddPolicy("AllowAnyOrigin", (CorsPolicyBuilder obj) => obj.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            services.AddAutoMapper(typeof(StartUp));
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>();
       
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IAuthorAuthService, AuthorAuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<Profile, MappingProfile>();
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

            app.UseCors("AllowAnyOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

