using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WenElevating.Todo.Services.Bases;

namespace WenElevating.Todo.Services
{
    public class ApplicationConfigurationService : CongurationServiceBase
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;    
        }

        public override object? GetConfiguration(Type type,string key) 
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(key);
            return _configuration.GetValue(type, key);
        }

        public override object? GetConfiguration<T>(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            return _configuration.GetValue<T>(key);
        }
    }
}
