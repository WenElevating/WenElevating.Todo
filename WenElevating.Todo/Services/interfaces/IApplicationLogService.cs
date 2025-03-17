using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Services.interfaces
{
    public interface IApplicationLogService : ILogService
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        void LoadingConfiguration();
    }
}
