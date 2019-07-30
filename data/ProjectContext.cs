using System;
using Microsoft.EntityFrameworkCore;
using data.Entities;

namespace data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            
        }
        
        DbSet<Questionnaire> Questionnaire { get; set; }  

    }
}
