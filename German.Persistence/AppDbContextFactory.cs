

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//manually build configuratuion because the persistence project is out the mvc
namespace German.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}German.API", Path.DirectorySeparatorChar);
            //expected to result change to directorypath/german.web

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            return new AppDbContext(configuration);
        }
    }
}