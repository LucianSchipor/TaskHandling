using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class Add_TaskVM : INotifyPropertyChanged
    {
        public TaskVM taskVM { get; set; }
        Task newTask;
        ObservableCollection<Task> taskList;

        public Add_TaskVM(ObservableCollection<Task> taskList)
        {
            selectedDate = DateTime.Now.ToString();

            newTask = new Task();
            taskVM = new TaskVM(newTask);
            this.taskList = taskList;
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
        private void SetName(object param)
        {
            var dialog = new InputDialog("Name");
            if (dialog.ShowDialog() == true)
            {
                taskVM.TaskName = dialog.Answer;
            }
        }

        public void CreateTask(object param)
        {
            this.taskList.Add(taskVM.task);
            this.NotifyPropertyChanged(nameof(taskList));
        }

        private void SetPriority(object param)
        {
            var textBlock = param as TextBlock;
            taskVM.TaskPriority = textBlock.Text;
        }

        private void SetType(object param)
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

        private void SetDeadLine(object param)
        {
            if (selectedDate != null)
            {
                taskVM.DeadLine = DateTime.Parse(selectedDate);
            }
        }

        private void SetDescription(object param)
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
