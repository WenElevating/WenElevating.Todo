using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Models
{
    public abstract class BaseTask
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public DateTime UpdatedTime { get; set; }

        public string CreateUser { get; set; } = "";

        protected BaseTask(string title, string content, DateTime updateTime, string createUser)
        {
            Title = title;
            Content = content;
            UpdatedTime = updateTime;
            CreateUser = createUser;
        }

        protected BaseTask(string title, string content, string createUser) : this(title, content, DateTime.Now, createUser)
        {
        }

        protected BaseTask(string title, string content) : this(title, content, "Admin")
        {

        }
    }
}
