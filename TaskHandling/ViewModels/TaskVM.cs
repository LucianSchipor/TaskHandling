using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TaskHandling.Models;

namespace TaskHandling.ViewModels
{
    public class TaskVM : INotifyPropertyChanged
    {
        public Task task;
        public TaskVM(Task task)
        {
            this.task = task;
        }

        public string TaskName
        {
            get
            {
                return task.taskName;
            }
            set
            {
                task.taskName = value;
                NotifyPropertyChanged(nameof(task.taskName));
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get
            {
                return task.taskDescription;
            }
            set
            {
                task.taskDescription = value;
                NotifyPropertyChanged(nameof(task.taskDescription));
            }
        }

        private string _taskType;
        public string TaskType
        {
            get
            {
                return task.taskType;
            }
            set
            {
                task.taskType = value;
                NotifyPropertyChanged(nameof(task.taskType));
            }
        }

        private string _taskStatus;
        public string TaskStatus
        {
            get
            {
                return task.taskStatus;
            }
            set
            {
                task.taskStatus = value;
                NotifyPropertyChanged(nameof(task.taskStatus));
            }
        }

        private string _taskPriority;
        public string TaskPriority
        {
            get
            {
                return task.taskPriority;
            }
            set
            {
                task.taskPriority = value;
                NotifyPropertyChanged(nameof(task.taskPriority));
            }
        }

        private DateTime _deadline;
        public DateTime DeadLine
        {
            get
            {
                return task.deadLine;
            }
            set
            {
                task.deadLine = value;
                NotifyPropertyChanged(nameof(task.deadLine));
            }
        }

        private DateTime _doneTime;
        public DateTime TaskDoneTime
        {
            get
            {
                return task.taskDoneTime;
            }
            set
            {
                task.taskDoneTime = value;
                NotifyPropertyChanged(nameof(task.taskDoneTime));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
