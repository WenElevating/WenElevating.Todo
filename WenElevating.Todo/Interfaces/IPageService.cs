using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WenElevating.Todo.Interfaces
{
    /// <summary>
    /// Page管理接口
    /// 1. 初始化page字典
    /// 2. 根据Tag获取Page
    /// </summary>
    public interface IPageService
    {
        public Page? GetPageByTag(string tag);
    }
}
