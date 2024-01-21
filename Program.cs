using JsonTree.Data;
using JsonTree.Interfaces;
using JsonTree.Repositories;
using JsonTree.Services;
using Microsoft.EntityFrameworkCore;

namespace JsonTree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IObjectRepository, ObjectRepository>();
            builder.Services.AddTransient<ObjectTxtConverterService>();
            builder.Services.AddTransient<ConfigurationKeyService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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

            app.MapWhen(context => context.Request.Path.Value.Contains("key"), appBuilder =>
            {
                appBuilder.UseRouting();
                appBuilder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "keyRoute",
                        pattern: "{*keys}",
                        defaults: new { controller = "Home", action = "Index" });
                });
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
