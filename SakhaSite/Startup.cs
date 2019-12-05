using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SakhaSite.Models.DataModels;
using SakhaSite.Contexts;
using SakhaSite.Extensions;
using SakhaSite.Helpers;
using SakhaSite.MapperProfiles;
using SakhaSite.Models.ViewModels.Validators;
using SakhaSite.Repository;

namespace SakhaSite
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new DefaultProfile());
            }, this.GetType().Assembly);

            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton(appSettings);


            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionString);
            });
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionString);
            });

            services.AddTransient<IProductsRepository, ProductsRepository>();

            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<ChangePasswordViewModelValidator>();
                })
                .AddRazorRuntimeCompilation();


            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 3;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.SlidingExpiration = true;
                options.ReturnUrlParameter = "ReturnUrl";
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost",
                    ValidAudience = "http://localhost",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RoleClaimType = "Roles",
                    NameClaimType = "UserName",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("my secure password")),
                };
            });


            services.AddAuthorization();

            services.AddTransient<PagingHelper>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MigrateDatabase<DataContext>();
            app.MigrateDatabase<AuthDbContext>();
            DataSeeder.SeedRoles(app);
            DataSeeder.SeedUsers(app);

            app.UseRouting();
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                      "{controller=Home}/{action=Index}/{id?}");
            });

        }

    }

}
