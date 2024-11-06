using BlogSite.Service.Concretes;
using Core.Tokens.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application;
using Application.Services.Repositories;
using Domain.Entities;
using Infrastructure.Services.Abstracts;
using Persistance.Contexts;
using Persistance.Repositories;
using System.Configuration;
using System.Reflection;
using Infrastructure.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOption"));


builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICategoryRepository, CategoryAsyncRepository>();
builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();


builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<BaseDbContext>();

builder.Services.AddHttpContextAccessor();

var tokenOption = builder.Configuration.GetSection("TokenOption").Get<TokenOption>();
var securityKey = SecurityKeyHelper.GetSecurityKey(tokenOption.SecurityKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOption.Issuer,
        ValidAudiences = tokenOption.Audience,
        IssuerSigningKey = securityKey
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();


async Task CreateRolesAsync(WebApplication app)
{
    var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = app.Services.GetRequiredService<UserManager<User>>();

    string[] roleNames = { "User", "Admin" }; 
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var role = new IdentityRole(roleName);
            await roleManager.CreateAsync(role);
        }
    }
}