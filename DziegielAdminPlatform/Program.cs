using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Endpoints;
using DziegielAdminPlatform.Extensions;
using DziegielAdminPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddAuthServices();

var app = builder.Build();

app.MapCustomIdentityApi<PlatformUser>();
app.MapAdminEndpoint();
app.MapEntitiesEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.SeedRoles();
app.SeedUsers();

app.Run();