using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Data;
using GamifiedProgrammingAcademy.API.Services;
using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using GamifiedProgrammingAcademy.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GamifiedAcademyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Registrar repositorios
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBadgeRepository, BadgeRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBadgeService, BadgeService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
// Registrar servicios
builder.Services.AddScoped<IChallengeService, ChallengeService>();


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
