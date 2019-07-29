using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DynamicForm.Helpers.Configuration
{
    public static class DefaultConfigurationLoader
    {
//        public static IConfiguration Load()
//        {
//            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//            var isDevelopment = environment == EnvironmentName.Development;
//
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{environment}.json", optional: true)
//                .AddEnvironmentVariables();
//
//            if(isDevelopment)
//                builder.AddUserSecrets<Startup>();
//
//            return builder.Build();
//        }
    }
}