using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Utilties
{
    public class RandomUtil
    {
        public static readonly Random random = new();

        /// <summary>
        /// 随机值（包含起始位）
        /// </summary>
        /// <param name="end"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int Number(int start, int end)
        {
            return random.Next(start, end);
        }

        /// <summary>
        /// 随机值（起始位为0）
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int Number(int end)
        {
            return random.Next(end);
        }
    }
}
