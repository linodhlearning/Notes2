using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notes.Api.Handlers;
using Notes.Api.Model;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Notes.Api.Repository.Entities;

var builder = WebApplication.CreateBuilder(args);

// azure app configuration and feature management
var appConfigConnStr = "Endpoint=https://linappconfig.azconfig.io;Id=IIIIIIIIII;Secret=SSSSSSSSSSSSSSSSSSS";
builder.Host.ConfigureAppConfiguration(b =>
b.AddAzureAppConfiguration(
    options => options.Connect(appConfigConnStr).UseFeatureFlags()
)
);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ErrorModel).Assembly));
// Add Dbcontext class
//var options = new DbContextOptionsBuilder<NotesDBContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

builder.Services.AddDbContext<NotesDBContext>(options => options.UseInMemoryDatabase(databaseName: "database_name"));

builder.Services.AddFeatureManagement();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
