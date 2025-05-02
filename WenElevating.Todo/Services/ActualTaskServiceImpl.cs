using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Enums;
using WenElevating.Todo.Models;
using WenElevating.Todo.Repositories.interfaces;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.Services
{
    public class ActualTaskServiceImpl : ITaskService<ActualTask>
    {
        private readonly ITaskRespository<ActualTask> _taskRespository;

        public ActualTaskServiceImpl(ITaskRespository<ActualTask> taskRespository)
        {
            _taskRespository = taskRespository;
        }

        public TaskResultCode Create(ActualTask task)
        {
            ArgumentNullException.ThrowIfNull(task, nameof(task));

            try
            {
                task.CreatedTime = DateTime.Now;
                task.UpdatedTime = DateTime.Now;

                if (_taskRespository.Create(task))
                {
                    // 发送消息通知 todo
                    return TaskResultCode.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return TaskResultCode.Failed;
        }

        public TaskResultCode Delete(ActualTask task)
        {
            ArgumentNullException.ThrowIfNull(task, nameof(task));

            try
            {
                if (_taskRespository.Delete(task))
                {
                    return TaskResultCode.Success;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
            return TaskResultCode.Failed;
        }

        public List<ActualTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public ActualTask GetById(string id)
        {
            throw new NotImplementedException();
        }

        public ActualTask GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public TaskResultCode Update(ActualTask task)
        {
            throw new NotImplementedException();
        }
    }
}
