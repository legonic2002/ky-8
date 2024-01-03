namespace BasicCrud.Data
{
    using BasicCrud.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class BasicCrudDbContext : DbContext
    {
        public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}