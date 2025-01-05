using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Endpoints;
using DziegielAdminPlatform.Extensions;
using DziegielAdminPlatform.Middlewares;
using DziegielAdminPlatform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IRoleService, RoleService>();
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
else
{
    app.UseHttpsRedirection();
}

app.UseSession();
app.UseAuth();

await app.SeedDziegielPlatform();

app.Run();