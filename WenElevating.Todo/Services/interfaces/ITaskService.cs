using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Enums;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Services.interfaces
{
    /// <summary>
    /// 任务服务接口
    /// </summary>
    public interface ITaskService<T> where T : BaseTask
    {
        public TaskResultCode Create(T task);

        public TaskResultCode Delete(T task);

        public TaskResultCode Update(T task);

        public T GetById(string id);

        public T GetByName(string name);

        public List<T> GetAll();
    }
}
