using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace data.design
{
    public class DesignTime : IDesignTimeDbContextFactory<ProjectContext>
    {
        public ProjectContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddUserSecrets(Assembly.GetExecutingAssembly());
            configurationBuilder.AddEnvironmentVariables();
            var config = configurationBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DynamicForm;Integrated Security=True");
            return new ProjectContext(optionsBuilder.Options);
        }
    }
}
