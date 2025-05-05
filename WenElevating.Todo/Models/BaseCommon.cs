using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Models
{
    public abstract class BaseCommon
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDeleted { get; set; } = 0;
    }
}
