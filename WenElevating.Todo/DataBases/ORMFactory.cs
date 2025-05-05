using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.DataBases
{
    public class ORMFactory : IORMFactory
    {
        public IDataBaseORM Create(int dataBaseType, string connectionString)
        {
            if (dataBaseType == -1)
            {
                throw new ArgumentException("ORM type cannot be -1");
            }

            switch (dataBaseType)
            {
                case 0:
                    return new FreeSqlORM(dataBaseType ,connectionString);
                default:
                    throw new NotSupportedException($"ORM type {dataBaseType} is not supported.");
            }
        }
    }
}
