using CsvHelper.Configuration;
using System.Globalization;
using WineriesApp.DataContext;
using WineriesApp.Api.Infrastructure.Startup;
using WineriesApp.Common.Filters;
using WineriesApp.Common.Pipes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(builder.Environment, builder.Configuration);

// Configure gRPC clients
builder.Services.ConfigureGrpcClients(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.ApplyMigrations();

// Configure Search
await app.Services.ConfigureSearch();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowLocalhost3000");
}
else
{
    app.UseCors("AllowProductionFrontend");
}


app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler("/api/error/home");

app.Run();
