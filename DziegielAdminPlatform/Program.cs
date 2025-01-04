using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Endpoints;
using DziegielAdminPlatform.Middlewares;
using DziegielAdminPlatform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IPasswordStore, PasswordStore>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISessionManager, SessionManager>();

var app = builder.Build();

app.MapAdminEndpoint();
app.MapEntitiesEndpoint();
app.MapAuthEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuth();

app.Run();