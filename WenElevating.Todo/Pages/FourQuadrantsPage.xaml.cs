﻿using System;
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
using WenElevating.Todo.Attributies;

namespace WenElevating.Todo.Pages
{
    /// <summary>
    /// FourQuadrantsPage.xaml 的交互逻辑
    /// </summary>
    [NavigationPageInfo("Todo_FourQuadrantsIcon", "Todo_FourQuadrantsIconSelected", "四象限", "FourQuadrants", 20, 25)]
    public partial class FourQuadrantsPage : ApplicationPageBase
    {
        public FourQuadrantsPage()
        {
            InitializeComponent();
        }
    }
}
