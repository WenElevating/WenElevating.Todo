using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.DataBases
{
    public interface IORMFactory
    {
        public IDataBaseORM Create(int dataBaseType, string connectionString);
    }
}
