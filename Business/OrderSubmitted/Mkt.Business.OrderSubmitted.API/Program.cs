using Mkt.Business.OrderSubmitted.Application.Kafka;
using Mkt.Business.OrderSubmitted.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<KafkaClient, KafkaClient>();
builder.Services.AddHttpClient<ManagmentService>();
builder.Services.AddTransient<ManagmentService, ManagmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
