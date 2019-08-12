using System.Text;
using data;
using DynamicForm.Helpers.Configuration;
using DynamicForm.Helpers.Extension.StartUp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using VueCliMiddleware;

namespace DynamicForm
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { _configuration = configuration; }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = services.AddConfig(_configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbAsIdentity(config);
            //services.AddDbContext<ProjectContext>(opts => opts.UseSqlServer(config.Data.ConnectionString));

            //services.AddIdentity<ApplicationUser,IdentityRole>()

            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,

                            //ValidIssuer = "12345678",
                            ValidateAudience = false,

                            //ValidAudience ="12345678",
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567812345678"))
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(

                    // swagger doc name, Effect SwaggerDocument URL Position。
                    name: "v1",

                    // info:  SwaggerDocument Version Info (Optional)。
                    info: new Info
                    {
                        Title = "Swagger",
                        Version = "1.0.0",
                        Description = "This is ASP.NET Core API Sample.",
                        TermsOfService = "None",
                        Contact = new Contact {Name = "Wing", Url = "https://"},
                        License = new License {Name = "License", Url = "https://"}
                    });

                c.AddSecurityDefinition("Bearer",
                                        new ApiKeyScheme
                                        {
                                            Description =
                                                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                            Name = "Authorization",
                                            In = "header",
                                            Type = "apiKey"
                                        });
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {
            var config = app.ApplicationServices.GetService<CustomConfiguration>();

            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
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

                    // description: For Swagger UI Select Different SwaggerDocument 。
                    name: " API Document v1.0.0");
            });

            //  if(config.Security.EnableHttps)
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}"); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) { spa.UseVueCli(npmScript: "serve"); }
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
