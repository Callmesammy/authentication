
using AuthXY.Endpoints;
using AuthXY.Models;
using AuthXY.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// 🔹 Database
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("AuthXDb"));

// 🔹 Identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 🔹 JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// 🔹 Services
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// 🔹 Middlewares
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Endpoints
app.MapAuthEndpoints();

app.Run();
