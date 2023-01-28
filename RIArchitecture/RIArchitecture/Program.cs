using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RIArchitecture.Application.Administration.Seeding;
using RIArchitecture.Core;
using Serilog;

namespace RIArchitecture.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Debug()
               .WriteTo.File("Logs/logs.txt")
               .CreateLogger();

            try
            {
                // Seeding Default users and roles to system                
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                    await PrismMasonRoleSeeder.SeedAsync(userManager, roleManager);
                    await PrismMasonUserSeeder.SeedBasicUserAsync(userManager, roleManager);
                    await PrismMasonUserSeeder.SeedSuperAdminAsync(userManager, roleManager);
                    Log.Information("Finished Seeding Default Data");
                }
                Log.Information("Starting Web Host");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
