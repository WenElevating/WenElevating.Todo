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
        private IDataBaseORM? _orm;
        public TaskRepository(FreeSqlORM orm)
        {
            _orm = orm;
        }
        public bool Create(ActualTask task)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ActualTask task)
        {
            throw new NotImplementedException();
        }

        public List<ActualTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public ActualTask? GetById(string id)
        {
            throw new NotImplementedException();
        }

        public ActualTask? GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public bool Update(ActualTask task)
        {
            throw new NotImplementedException();
        }
    }
}
