using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Models
{
    public class ActualTask : BaseTask
    {
        /// <summary>
        /// 子任务列表
        /// </summary>
        public List<ActualTask> childTasks = [];

        /// <summary>
        /// 标签列表
        /// </summary>
        public List<string> Tags { get; set; } = [];

        /// <summary>
        /// 优先级
        /// </summary>
        public int Prority { get; set; } = 0;

        /// <summary>
        /// 是否有附件
        /// </summary>
        public bool HasAnnexFiles { get; set; }

        /// <summary>
        /// 今日是否已完成
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecuteTime { get; set; } = "";

        public ActualTask(string title, string content) : base(title, content)
        {
        }

        public ActualTask(string title, string content, string createUser) : base(title, content, createUser)
        {
        }

        public ActualTask(string title, string content, List<string> tagList, bool hasAnnexFiles,string executeTime, string createUser, int prority = 0) : base(title, content, DateTime.Now, createUser)
        {
            Tags = tagList;
            ExecuteTime = executeTime;
            HasAnnexFiles = hasAnnexFiles;
            Prority = prority;
        }

        public override bool Equals(object? obj)
        {
            return obj is ActualTask task &&
                   Id == task.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
