using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Services
{
    internal class UserTaskService : IUserTaskService
    {
        private List<TaskClassification> _taskClassificationList;

        public UserTaskService()
        {
            _taskClassificationList = [];
        }

        public bool CollpaseClassification(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            ArgumentException.ThrowIfNullOrWhiteSpace(id);

            var item = _taskClassificationList.FirstOrDefault(item => item.Id == id);
            if (item == null)
            {
                return false;
            }

            // TODO 隐藏分类项

            return false;
        }

        public IEnumerable<TaskClassification> GetClassifications()
        {
            throw new NotImplementedException();
        }
    }
}
