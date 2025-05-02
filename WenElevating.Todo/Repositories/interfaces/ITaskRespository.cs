using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Repositories.interfaces
{
    public interface ITaskRespository<T> where T : BaseTask
    {
        public bool Create(T task);

        public bool Delete(T task);

        public bool Update(T task);

        public T GetById(string id);

        public T GetByName(string name);

        public List<T> GetAll();
    }
}
