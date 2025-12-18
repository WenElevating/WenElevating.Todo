using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Services
{
    internal interface IUserTaskService
    {
        /// <summary>
        /// 获取当前用户的任务分类（加载本地配置）
        /// </summary>
        /// <returns>分类列表</returns>
        public IEnumerable<TaskClassification> GetClassifications();
        
        /// <summary>
        /// 隐藏指定分类
        /// </summary>
        /// <param name="id">分类id</param>
        /// <returns>操作结果</returns>
        public bool CollpaseClassification(string id);
    }
}
