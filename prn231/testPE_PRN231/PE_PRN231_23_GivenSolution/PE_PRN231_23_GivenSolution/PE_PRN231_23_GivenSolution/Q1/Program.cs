using Microsoft.AspNetCore.OData;
using Q1.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(x=>
x.Select()
.Filter()
.OrderBy()
.Expand()
.SetMaxTop(100));
//http://localhost:5100/api/Product/index?top=2
//http://localhost:5100/api/Product/index?$filter=productName eq 'Sasquatch Ale'
//http://localhost:5100/api/Product/index?$orderby=unitPrice asc
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
    x=> x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseODataBatching();
app.MapControllers();

app.Run();
