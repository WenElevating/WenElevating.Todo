using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using WenElevating.Todo.Commons.Helpers;
using WenElevating.Todo.Models;
using WenElevating.Todo.Services;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.ViewModels
{
    public class TaskPageViewModel : ObservableRecipient
    {
        private readonly string _classificationPrefix = "Todo_NoteList_";

        private readonly IApplicationLogService _logService;

        public ObservableCollection<TaskClassification> Classifications
        {
            get; 
            set;
        }

        public TaskPageViewModel(IApplicationLogService logService)
        {
            _logService = logService;
            Classifications = LoadingClassifications(App.Current.settings?.NoteClassification);
        }

        private ObservableCollection<TaskClassification> LoadingClassifications(List<string>? classifications)
        {
            _logService.LogInfo("正在加载笔记分类...");
            ObservableCollection<TaskClassification> classifciationCollection = [];

            classifications?.ForEach((item) =>
            {
                // Todo 获取指定分类日志数量
                var key = _classificationPrefix + item;
                var task = new TaskClassification(item, item == "Today" ? key + "_" + DateTime.Today.Day: key, LocalizationHelper.GetLocalizationString(key));
                classifciationCollection.Add(task);
            });

            _logService.LogInfo("已完成笔记分类加载...");
            return classifciationCollection;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
