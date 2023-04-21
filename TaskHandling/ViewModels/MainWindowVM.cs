using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class MainWindowVM: INotifyPropertyChanged
    {
        public ObservableCollection<TDL> tdlList { get; set; }

        public MainWindowVM() {
            tdlList = new ObservableCollection<TDL>();
            tdlList.Add(new TDL() { Name = "scoala" });
            tdlList.Add(new TDL() { Name = "masini" });
            tdlList.Add(new TDL() { Name = "sula" });
            tdlList[0].TasksCollection.Add(new Task() { taskName = "Du-te la scoala", taskDescription = "mai repede" });
            tdlList[0].TdlCollection.Add(new TDL() { Name = "hei prieteni", Image = "../Assets/TDLIcons/School" });
            tdlList[0].TdlCollection[0].TdlCollection.Add(new TDL() { Name = "ai reusit" });
        }
        
       public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            MessageBox.Show("BV");
            addWindow.Show();
        }

        private ICommand addRootCommand;
        public ICommand AddRootCommand
        {
            get
            {
                if (addRootCommand == null)
                {
                    addRootCommand = new RelayCommand(Add_Root_TDL);
                }
                return addRootCommand;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
