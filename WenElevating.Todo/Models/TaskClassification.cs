using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WenElevating.Todo.Models
{
    /// <summary>
    /// 任务分类
    /// </summary>
    public class TaskClassification : INotifyPropertyChanged
    {
        public string Id { get; set; }

        public DrawingImage? Icon { get; set; }

        public string Title { get; set; }

        private int _noteCount;
        public int NoteCount
        {
            get => _noteCount;
            set
            {
                if (_noteCount != value)
                {
                    _noteCount = value;
                    OnPropertyChanged(nameof(NoteCount));
                }
            }
        }

        public TaskClassification(string id, string icon, string? title)
        {
            Id = id;
            Icon = (DrawingImage?)App.Current.Resources[icon] ?? (DrawingImage?)App.Current.Resources["NormalNoSelectedIcon"];
            Title = title ?? "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
