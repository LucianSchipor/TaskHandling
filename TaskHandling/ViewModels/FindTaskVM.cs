using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class FindTaskVM : INotifyPropertyChanged
    {
        string taskName;
        public TDL tdl;
        public ObservableCollection<Task> tasksList { get; set; }   
        public FindTaskVM(TDL tdlPrincipal)
        {
            this.tdl = tdlPrincipal;
        }

        public FindTaskVM()
        {

        }

        private ICommand findTaskCommand;
        public ICommand FindTaskCommand
        {
            get
            {
                if (findTaskCommand == null)
                {
                    findTaskCommand = new RelayCommand(FindTask);

                }
                return findTaskCommand;
            }
        }

        public void FindTask(object param)
        {
            InputDialog dialog = new InputDialog("");
            dialog.label.Content = "Task Name";
            var taskName = " ";
            if (dialog.ShowDialog() == true)
            {
                taskName = dialog.Answer;
            }
            this.taskName = taskName;

            tasksList = tdl.tdlservice.FindTaskInPrincipalTDL(this.taskName);
            OnPropertyChanged(nameof(tasksList));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
