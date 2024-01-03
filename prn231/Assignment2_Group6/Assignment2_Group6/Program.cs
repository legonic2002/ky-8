using Assignment2_Group6.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Category>("Categories");
modelBuilder.EntitySet<Member>("Members");
modelBuilder.EntitySet<Order>("Orders");
modelBuilder.EntitySet<OrderDetail>("OrderDetails");
modelBuilder.EntitySet<Product>("Products");

// Add services to the container.

builder.Services.AddControllers().AddOData(options => options.AddRouteComponents("odata", modelBuilder.GetEdmModel())
.Select().OrderBy().SetMaxTop(100).Filter().Count());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<eStoreContext>(option
    => option.UseSqlServer(conf.GetConnectionString("DbConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

