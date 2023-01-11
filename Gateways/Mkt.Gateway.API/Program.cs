using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.EnvironmentName.ToLower() == "development")
{
    builder.Configuration.AddJsonFile("ocelot.Development.json", optional: false, reloadOnChange: true);
}
if (builder.Environment.EnvironmentName.ToLower() == "docker")
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.Services.AddAuthentication();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();