﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HR_Management.Data;
using Microsoft.Extensions.Logging;
using HR_Management.Models;
using Microsoft.AspNetCore.Identity;

namespace HR_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            //host.Run();

            ////var host = BuildWebHost(args);
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<ApplicationDbContext>();
            //        var contect = services.GetRequiredService<UserManager<ApplicationUser>>();
            //        DbInitializer.Initialize(context, contect);
                    
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError("An error occurred while seeding the database." + ex);
            //    }
            //}

            host.Run();
        }
    }
}
