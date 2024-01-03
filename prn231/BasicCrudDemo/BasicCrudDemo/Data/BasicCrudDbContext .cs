
using Microsoft.EntityFrameworkCore;
using BasicCrudDemo;


namespace BasicCrudDemo.Data
{
    

    public class BasicCrudDbContext : DbContext
    {
        public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options)
            : base(options)
        {
        }
        
        //public DbSet<Customer> Customers { get; set; }
    }
}