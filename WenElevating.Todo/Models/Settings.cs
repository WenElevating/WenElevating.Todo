using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Models
{
    public class Settings
    {
        public string Version { get; set; }

        public int MaximumMemoryUsage { get; set; }
        
        public int MemoryMonitorInterval { get; set; }

        public int LogType { get; set; }

        public List<string> NoteClassification { get; set; }
    }
}
