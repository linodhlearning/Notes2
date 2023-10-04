using MediatR;
using Microsoft.AspNetCore.Hosting;
using Notes.Api.Handlers;
using Notes.Api.Model;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Repository.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ErrorModel).Assembly));
// Add Dbcontext class
//var options = new DbContextOptionsBuilder<NotesDBContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

builder.Services.AddDbContext<NotesDBContext>(options => options.UseInMemoryDatabase(databaseName: "database_name"));


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
