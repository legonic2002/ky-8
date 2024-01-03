using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Assignment01Solution_HE163971.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder =  new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Condiments" },
                new Category { CategoryId = 3, CategoryName = "Confections" },
                new Category { CategoryId = 4, CategoryName = "Dairy Products" },
                new Category { CategoryId = 5, CategoryName = "Grains/Cereals" },
                new Category { CategoryId = 6, CategoryName = "Meat/Poultry" },
                new Category { CategoryId = 7, CategoryName = "Produce" },
                new Category { CategoryId = 8, CategoryName = "Seafood" }

                );

            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, Email="1@gmail.com",CompanyName="A", City="Ha noi", Country="Viet Nam", Password="123456" },
                new Member { MemberId = 2, Email = "2@gmail.com", CompanyName = "B", City = "Ha noi", Country = "Viet Nam", Password = "123456" },
               new Member { MemberId = 3, Email = "3@gmail.com", CompanyName = "C", City = "Ha noi", Country = "Viet Nam", Password = "123456" },
               new Member { MemberId = 4, Email = "4@gmail.com", CompanyName = "D", City = "Ha noi", Country = "Viet Nam", Password = "123456" },
               new Member { MemberId = 5, Email = "5@gmail.com", CompanyName = "E", City = "Ha noi", Country = "Viet Nam", Password = "123456" },
               new Member { MemberId = 6, Email = "admin@gmail.com", CompanyName = "A", City = "Ha noi", Country = "Viet Nam", Password = "123456" }

                );
            modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.ProductId, x.OrderId });
        }
    }
}