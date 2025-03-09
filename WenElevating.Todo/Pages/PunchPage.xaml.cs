using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Pages
{
    /// <summary>
    /// PunchPage.xaml 的交互逻辑
    /// </summary>
    [NavigationPageInfo("Todo_PunchIcon", "Todo_PunchIconSelected", "习惯打卡", "Punch")]
    public partial class PunchPage : ApplicationPageBase
    {
        public PunchPage()
        {
            InitializeComponent();
        }
    }
}
