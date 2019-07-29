using System;
using Microsoft.EntityFrameworkCore;
using data.Entities;

namespace data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
            
        }
        
        DbSet<Questionnaire> Questionnaire { get; set; }  

    }
}
