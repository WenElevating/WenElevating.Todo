using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.DataBases
{
    public abstract class IDataBaseORM<T>
    {
        public abstract int Insert(T data);

        public abstract int Update(T data);

        public abstract int Delete(T data);

        public abstract T Get(string id);

        public abstract IEnumerable<T> GetAll();
    }
}
