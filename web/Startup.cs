using data;
using DynamicForm.Helpers.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using VueCliMiddleware;

namespace DynamicForm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configObject = services.AddConfig(_configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ProjectContext>(opts => opts.UseSqlServer(configObject.Data.ConnectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    // name: 攸關 SwaggerDocument 的 URL 位置。
                    name: "v1",
                    // info: 是用於 SwaggerDocument 版本資訊的顯示(內容非必填)。
                    info: new Info
                    {
                        Title = "Swagger",
                        Version = "1.0.0",
                        Description = "This is ASP.NET Core API Sample.",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Wing",
                            Url = "https://"
                        },
                        License = new License
                        {
                            Name = "Wing",
                            Url = "https://"
                        }
                    }
                );
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
                    {
                        configuration.RootPath = "ClientApp/dist";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {
            var config = app.ApplicationServices.GetService<CustomConfiguration>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //if(config.Security.EnableHttps)
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: " API Document v1.0.0"
                );
            });

            //  if(config.Security.EnableHttps)
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }
            });

            appLifeTime.ApplicationStopping.Register(CleanUp);
            Log.Information($"Test Project []");
        }

        private void CleanUp()
        {
            //Ensure log file closed.
            Log.Information("All custom services has stopped.");
            Log.CloseAndFlush();
        }
    }
}
