using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.DataBases;
using WenElevating.Todo.Models;
using WenElevating.Todo.Repositories.interfaces;

namespace WenElevating.Todo.Repositories
{
    public class TaskRepository : ITaskRespository<ActualTask>
    {
        private IDataBaseORM<ActualTask>? _orm;
        public TaskRepository(FreeSqlORM<ActualTask> orm)
        {
            _orm = orm;
        }

        public bool Create(ActualTask task)
        {
            ArgumentNullException.ThrowIfNull(task);
            return _orm?.Insert(task) == 1;
        }

        public bool Delete(ActualTask task)
        {
            ArgumentNullException.ThrowIfNull(task);
            return _orm?.Delete(task) == 1;
        }

        public List<ActualTask> GetAll()
        {
            return _orm?.GetAll().ToList() ?? [];
        }

        public ActualTask? GetById(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            return _orm?.Get(id);
        }

        public ActualTask? GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public bool Update(ActualTask task)
        {
            ArgumentNullException.ThrowIfNull(task);
            return _orm?.Update(task) == 1;
        }
    }
}
