using Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation
{
    public class AppConfig : IAppConfig
    {
        private readonly IConfigurationRoot _configuration;
        public IConfigurationRoot Configuration
        {
            get { return _configuration; }
            private set { }
        }

        public AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            Configuration = _configuration; 
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("WordDB");
        }

        public IConfigurationRoot GetConfiguration()
        {
            return Configuration;
        }
    }
}
