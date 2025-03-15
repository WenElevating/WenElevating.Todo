using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Services.Bases
{
    public abstract class CongurationServiceBase
    {
        public abstract object? GetConfiguration(Type type, string key);

        public abstract object? GetConfiguration<T>(string key);
    }
}
