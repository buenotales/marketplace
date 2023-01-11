using Microsoft.EntityFrameworkCore;
using Mkt.Domain.Product.Application.Infraestructure.Contexts;
using Mkt.Domain.Product.Application.Infraestructure.Repositories;
using Mkt.Domain.Product.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductContext>(options =>
{
    string connectionStringMysql = builder.Configuration.GetConnectionString("mkt-domain-product-db-mysql");
    options.UseMySql(connectionStringMysql, ServerVersion.AutoDetect(connectionStringMysql), b => b.MigrationsAssembly("Mkt.Domain.Product.API"));
});

builder.Services.AddTransient<ManagementService, ManagementService>();
builder.Services.AddTransient<ProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();