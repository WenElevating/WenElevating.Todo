using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.DataBases
{
    public abstract class IDataBaseORM
    {
        public abstract int Insert<T>(T data);

        public abstract int Update<T>(T data);

        public abstract int Delete<T>(T data);

        public abstract T Get<T>(string id);

        public abstract IEnumerable<T> GetAll<T>();
    }
}
