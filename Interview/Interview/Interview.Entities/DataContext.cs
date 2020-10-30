namespace Interview.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System;
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer("server=HAZERHAN\\SQLEXPRESS; " +
                                       "database=InterviewDB; " +
                                       "integrated security=true;");
        }
        public DbSet<Product> Products { get; set; }
    }
}
