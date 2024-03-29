﻿using Microsoft.Extensions.Configuration;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseConfigulation
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSettings = root.GetSection("ConnectionStrings:DefaultConnection");
            if (appSettings != null)
            {
                SqlConnectionString = appSettings.Value;
            }
        }

        public string SqlConnectionString { get; set; }
    }
}
