using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.DataBases
{
    public class FreeSqlORM<T> : IDataBaseORM<T> where T : BaseCommon
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

        public override int Delete(T data)
        {
            return Instance
                .Update<T>()
                .Set(item => item.IsDeleted, 1)
                .Where(item => item.Id == data.Id)
                .ExecuteAffrows();
        }

        public override T Get(string id)
        {
            return Instance
                .Select<T>()
                .Where<T>(item => item.Id == id)
                .First();
        }

        public override IEnumerable<T> GetAll()
        {
            return Instance
                .Select<T>()
                .Where(a => a.IsDeleted == 0)
                .ToList() ?? [];
        }

        public override int Insert(T data)
        {
            return Instance
                .Insert<T>(data)
                .ExecuteAffrows();
        }

        public override int Update(T data)
        {
            return Instance
                .Update<T>(data)
                .ExecuteAffrows();
        }
    }
}
