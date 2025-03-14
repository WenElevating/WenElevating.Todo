using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WenElevating.Todo.Services
{
    public class ApplicationConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;    
        }

        public object? GetConfiguration(Type type,string key) 
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(key);
            return _configuration.GetValue(type, key);
        }
    }
}
