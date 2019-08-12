using System;
using data;
using data.model;
using DynamicForm.Helpers.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicForm.Helpers.Extension.StartUp
{
    public static class IdentityDbExtension
    {
        public static void AddDbAsIdentity(this IServiceCollection services, CustomConfiguration config)
        {
            services.AddDbContext<ProjectContext>(options =>
            {
                options.UseSqlServer(config.Data.ConnectionString,
                                     opt => { opt.EnableRetryOnFailure(); });
            });

            services.AddIdentity<User, Role>(options =>
                    {
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;

                        options.Lockout.DefaultLockoutTimeSpan =
                            TimeSpan.FromMinutes(config.Security.Lockout.LockoutMinutes);

                        options.Lockout.MaxFailedAccessAttempts = config.Security.Lockout.MaxFailedLogin;
                        options.Lockout.AllowedForNewUsers = true;
                    })
                    .AddEntityFrameworkStores<ProjectContext>();
        }
    }
}
