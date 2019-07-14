using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAppConfig
    {
        string GetConnectionString();
        IConfigurationRoot GetConfiguration();
    }
}
