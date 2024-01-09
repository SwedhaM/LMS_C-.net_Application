//Title : Library Management System
//Created On : 
//Updated On :
//Reviewed By :
//Reviewed On :
//Author : Swedha M

using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Data.Repositories;
using Serilog;
using LibraryManagementSystem.Filters;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            

var configuration=builder.Configuration;
string? connection=builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromSeconds(5);
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;
// });
// builder.Services.AddScoped<ResultFilter>();
// builder.Services.AddMvc(options =>
// {
//     options.Filters.Add<ResultFilter>();
// });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<BookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<UserDetailDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<OrderDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<BorrowBookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<IssueBookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<SignupDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/Home/LoginPage";
//         options.AccessDeniedPath = "/Account/AccessDenied";
//     });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/Home/LoginPage";
//         options.AccessDeniedPath = "/Account/AccessDenied";
//         options.ExpireTimeSpan = TimeSpan.FromDays(30); // Set cookie expiration time
//         options.SlidingExpiration = true; // Enable sliding expiration
//     });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "LibraryManagementSystem";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.LoginPath = "/Login/AdminLogin";
        options.AccessDeniedPath = "/Home/AccessDenied";
        //options.ExpireTimeSpan = TimeSpan.FromDays(30);
        //options.SlidingExpiration = true;
    });




            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var apiEndpoint = configuration["ApiEndpoint"];
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(apiEndpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("LibraryManagementSystem/json"));
            });
// Log.Logger=new LoggerConfiguration().WriteTo.File("C:\\Users\\Swedha M\\Desktop\\LibraryManagementSystem\\Logs\\Logger-.log",rollingInterval:RollingInterval.Day).CreateLogger();
// builder.Logging.AddSerilog(_logger);

var _logger =new LoggerConfiguration().
WriteTo.File("C:\\Users\\Swedha M\\Desktop\\LibraryManagementSystem\\Logs\\Logger-.log",rollingInterval:RollingInterval.Day).CreateLogger();
builder.Logging.AddSerilog(_logger);
          
          
            // builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options =>
            //     {
            //         options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            //         options.LoginPath = new PathString("/Account/Login");
            //     });
//Logging
            // var _logger = new LoggerConfiguration().
            // WriteTo.File("C:\\Users\\Swedha M\\Desktop\\LibraryManagementSystem\\Logs\\Logger-.log", rollingInterval: RollingInterval.Day).CreateLogger();
            // builder.Logging.AddSerilog(_logger);
            //var _loggrer = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
            // .MinimumLevel.Error()
            // .WriteTo.File("F:\\LaernCore\\Logs\\ApiLog-.log",rollingInterval:RollingInterval.Day)
            
            //builder.Logging.AddSerilog(_loggrer);
            // Log.Logger=new LoggerConfiguration().WriteTo.File("C:\\Users\\Swedha M\\Desktop\\LibraryManagementSystem\\Logs\\Logger-.log",RollingInterval:RollingInterval.Day)
            // .CreateLogger()

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}