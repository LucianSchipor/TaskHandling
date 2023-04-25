using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Services;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class Add_TaskVM : INotifyPropertyChanged
    {
        public TaskVM taskVM { get; set; }
        private TDL tdl;
        Task newTask;
        ObservableCollection<Task> taskList;

        public Add_TaskVM(TDL tdl, ObservableCollection<Task>TaskList)
        {
            newTask = new Task();
            this.tdl = tdl;
            taskVM = new TaskVM(newTask);
            this.taskList = TaskList;
        }
        
        public Add_TaskVM()
        {

        }

        private ICommand setTaskDescriptionCommand;
        public ICommand SetTaskDescriptionCommand
        {
            get
            {
                if (setTaskDescriptionCommand == null)
                {
                    setTaskDescriptionCommand = new RelayCommand(SetDescription);
                }
                return setTaskDescriptionCommand;
            }
        }

        private ICommand setTaskNameCommand;
        public ICommand SetTaskNameCommand
        {
            get
            {
                if (setTaskNameCommand == null)
                {
                    setTaskNameCommand = new RelayCommand(SetName);
                }
                return setTaskNameCommand;
            }
        }

        private ICommand setDeadLineCommand;
        public ICommand SetDeadLineCommand
        {
            get
            {
                if (setDeadLineCommand == null)
                {
                    setDeadLineCommand = new RelayCommand(SetDeadLine);
                }
                return setDeadLineCommand;
            }
        }


        private ICommand setTaskPriority;
        public ICommand SetTaskPriority
        {
            get
            {
                if (setTaskPriority == null)
                {
                    setTaskPriority = new RelayCommand(SetPriority);
                }
                return setTaskPriority;
            }
        }

        private ICommand createTaskCommand;
        public ICommand CreateTaskCommand
        {
            get
            {
                if (createTaskCommand == null)
                {
                    createTaskCommand = new RelayCommand(CreateTask);
                }
                return createTaskCommand;
            }
        }

        private ICommand setTaskType;
        public ICommand SetTaskType
        {
            get
            {
                if (setTaskType == null)
                {
                    setTaskType = new RelayCommand(SetType);
                }
                return setTaskType;
            }
        }
        public void SetName(object param)
        {
            var dialog = new InputDialog("Name");
            if (dialog.ShowDialog() == true)
            {
                taskVM.TaskName = dialog.Answer;
            }
        }

        public void CreateTask(object param)
        {
            selectedDate = DateTime.Now.ToString();
            this.taskList.Add(taskVM.task);
            this.tdl.tdlservice.SaveToFile(null);
            //imi salveaza in fisier tdl-ul selectat, dar il inlocuieste
            this.NotifyPropertyChanged(nameof(taskList));
            
        }

      

        public void SetPriority(object param)
        {
            var textBlock = param as TextBlock;
            taskVM.TaskPriority = textBlock.Text;
        }

        public void SetType(object param)
        {
            var textBlock = param as TextBlock;
            taskVM.TaskType = textBlock.Text;
        }


        private string selectedDate;
        public string SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                NotifyPropertyChanged(nameof(selectedDate));
            }
        }

        public void SetDeadLine(object param)
        {
            if (selectedDate != null)
            {
                taskVM.DeadLine = DateTime.Parse(selectedDate);
            }
        }

        public void SetDescription(object param)
        {
            var dialog = new InputDialog("Description");
            if (dialog.ShowDialog() == true)
            {
                taskVM.TaskDescription = dialog.Answer;
                NotifyPropertyChanged(nameof(taskVM.TaskDescription));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
