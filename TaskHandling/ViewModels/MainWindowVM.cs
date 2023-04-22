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
        public TDL _tdl { get; set; }
        public Add_TDLVM ADD_TDLVM { get; set; }

        public MainWindowVM() {
            _tdl = new TDL();
            _tdl.TdlCollection.Add(new TDL() { Name = "scoala" });
            _tdl.TdlCollection.Add(new TDL() { Name = "masini" });
            _tdl.TdlCollection.Add(new TDL() { Name = "sula" });
            _tdl.TdlCollection[0].TasksCollection.Add(new Task() { taskName = "Du-te la scoala", taskDescription = "mai repede" });
            _tdl.TdlCollection[0].TdlCollection.Add(new TDL() { Name = "hei prieteni", Image = "../Assets/TDLIcons/School" });
            _tdl.TdlCollection[0].TdlCollection.Add(new TDL() { Name = "ai reusit" });

            ADD_TDLVM = new Add_TDLVM(_tdl);
        }
        
       public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            addWindow.DataContext = ADD_TDLVM;
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
