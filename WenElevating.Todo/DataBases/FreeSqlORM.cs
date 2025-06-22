using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.DataBases
{
    public class FreeSqlORM : IDataBaseORM
    {
        private IFreeSql Instance;

        public FreeSqlORM(int type, string connectionString)
        {
            Instance = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString((FreeSql.DataType)type, connectionString)
                    .UseAdoConnectionPool(true)
                    .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))
                    .UseAutoSyncStructure(true) //自动同步实体结构到数据库，只有CRUD时才会生成表
                    .Build();
        }

        public override int Delete<T>(T data)
        {
            throw new NotImplementedException();
        }

        public override T Get<T>(string id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public override int Insert<T>(T data)
        {
            throw new NotImplementedException();
        }

        public override int Update<T>(T data)
        {
            throw new NotImplementedException();
        }
    }
}
