using System;
using Microsoft.EntityFrameworkCore;
using data.Entities;
using data.model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace data
{
    public class ProjectContext : IdentityDbContext<User, Role, Guid>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            
        }
        
        public DbSet<Questionnaire> Questionnaire { get; set; }  
        //public DbSet<User> AppUsers { get; set; }

    }
}
