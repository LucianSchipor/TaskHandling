using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Services;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class TreeViewModel : BaseVM
    {
        public TDL _tdl { get; set; }
        public TreeViewModel()
        {
            _tdl = new TDL();   
            _tdl.TdlCollection.Add(new TDL() { Name = "scoala" });
            _tdl.TdlCollection.Add(new TDL() { Name = "masini" });
            _tdl.TdlCollection.Add(new TDL() { Name = "sula" });
            _tdl.TdlCollection[0].TasksCollection.Add(new Task() { taskName = "Du-te la scoala", taskDescription = "mai repede" });
            _tdl.TdlCollection[0].TdlCollection.Add(new TDL() { Name = "hei prieteni", Image = "../Assets/TDLIcons/School" });
            _tdl.TdlCollection[0].TdlCollection.Add(new TDL() { Name = "ai reusit" });
            selectedItem = new TDL();
        }

        private TDL selectedItem;
        public TDL SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }
    }

    public class MainWindowVM: INotifyPropertyChanged
    {
        public Add_TDLVM ADD_TDLVM { get; set; }
        public ObservableCollection<Task> _tasksToView { get; set; }

        TDLService tdlService;
        public TreeViewModel treeViewModel { get; set; }

        public MainWindowVM() {
            treeViewModel = new TreeViewModel();
            _tasksToView = treeViewModel._tdl.TdlCollection[0].TasksCollection;
            tdlService = new TDLService(treeViewModel._tdl);
        }

        public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            addWindow.DataContext = ADD_TDLVM;
            MessageBox.Show("BV");
            addWindow.Show();
        }

        void viewTasks (object param)
        {
            _tasksToView = treeViewModel.SelectedItem.TasksCollection;
        }

        private ICommand itemDoubleClickedCommand;
        public ICommand ItemDoubleClickedCommand
        {
            get
            {
                if(itemDoubleClickedCommand == null)
                {
                    itemDoubleClickedCommand = new RelayCommand(SelectionChanged);
                }
                return itemDoubleClickedCommand;
            }
        }

            public void SelectionChanged(object sender)
            {
                if (sender is RoutedPropertyChangedEventArgs<TDL> args)
            {
                _tasksToView = args.NewValue.TasksCollection;
            }
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

        private ICommand viewTasksCommand;
        public ICommand ViewTasksCommand
        {
            get
            {
                if (viewTasksCommand == null)
                {
                    viewTasksCommand = new RelayCommand(viewTasks);
                }
                return viewTasksCommand;
            }
        }


        private void MoveUpTDL(object param)
        {
            tdlService.MoveTDL("up");
        }

        private ICommand moveUpTDLCommand;
        public ICommand MoveUpTDLCommand
        {
            get
            {
                if (moveUpTDLCommand == null)
                {
                    moveUpTDLCommand = new RelayCommand(MoveUpTDL);
                }
                return moveUpTDLCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
