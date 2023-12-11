using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeZoneDemoProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("CodeZoneProjectDB_ConStr");
            builder.Services.AddDbContext<IContext, Context>(options =>
                options.UseSqlServer(connectionString)
            );

            builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("CodeZoneProject.Application")));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("CodeZoneProject.Application"))));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}