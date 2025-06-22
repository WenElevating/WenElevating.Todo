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

namespace WenElevating.Resources.WebViews
{
    /// <summary>
    /// MarkdownEditor.xaml 的交互逻辑
    /// </summary>
    public partial class MarkdownEditor : UserControl
    {
        public string CodeLanguage
        {
            get { return (string)GetValue(CodeLanguageProperty); }
            set { SetValue(CodeLanguageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CodeLanguage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodeLanguageProperty =
            DependencyProperty.Register("CodeLanguage", typeof(string), typeof(MarkdownEditor), new PropertyMetadata("C#", CodeLanguageChangeCallback));

        public bool TextWrap
        {
            get { return (bool)GetValue(TextWrapProperty); }
            set { SetValue(TextWrapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextWrap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWrapProperty =
            DependencyProperty.Register("TextWrap", typeof(bool), typeof(MarkdownEditor), new PropertyMetadata(false, TextWrapChangeCallback));

        public MarkdownEditor()
        {
            InitializeComponent();
            Loaded += MarkdownEditor_Loaded;
        }

        private void MarkdownEditor_Loaded(object sender, RoutedEventArgs e)
        {
            //CustomMarkdownEditor.Source = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebViews", "Markdown.html"));
        }

        private static void CodeLanguageChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not MarkdownEditor control)
            {
                return;
            }

            if (e.NewValue is not string language)
            {
                throw new InvalidOperationException("The language must be string type!");
            }

            control.CodeLanguage = language;
        }

        private static void TextWrapChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not MarkdownEditor control)
            {
                return;
            }

            if (e.NewValue is not bool isWrap)
            {
                throw new InvalidOperationException("The language must be bool type!");
            }

            control.TextWrap = isWrap;
        }
    }
}
