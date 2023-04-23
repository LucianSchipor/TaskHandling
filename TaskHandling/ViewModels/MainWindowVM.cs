using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Services;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class TreeViewModel : BaseVM
    {

        public TreeViewModel()
        {
            _tdl = new TDL();

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

        private TDL _tdla;
        public TDL _tdl
        {
            get
            {
                return _tdla;   
            }
            set
            {
               _tdla = value;
                NotifyPropertyChanged(nameof(_tdla));
            }
        }


    }

    public class MainWindowVM : INotifyPropertyChanged
    {
        public Add_TDLVM ADD_TDLVM { get; set; }
        public TDL selectedTDL;

        public ObservableCollection<Task> _tasksToView { get; set; }

        TDLService tdlService;
        public TreeViewModel treeViewModel { get; set; }

        public MainWindowVM()
        {
            treeViewModel = new TreeViewModel();
            tdlService = new TDLService(treeViewModel._tdl);
            ADD_TDLVM = new Add_TDLVM(treeViewModel._tdl);

        }

        public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            addWindow.DataContext = ADD_TDLVM;
            MessageBox.Show("BV");
            addWindow.Show();
        }

        void viewTasks(object param)
        {
            _tasksToView = treeViewModel.SelectedItem.TasksCollection;
        }

        private ICommand itemDoubleClickedCommand;
        public ICommand ItemDoubleClickedCommand
        {
            get
            {
                if (itemDoubleClickedCommand == null)
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

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                    if (_isSelected)
                    {
                    }
                }
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

        private ICommand archiveDatabaseCommand;
        public ICommand ArchiveDatabaseCommand
        {
            get
            {
                if (archiveDatabaseCommand == null)
                {
                    archiveDatabaseCommand = new RelayCommand(SaveToFile);
                }
                return archiveDatabaseCommand;
            }
        }

        private ICommand openDatabaseCommand;
        public ICommand OpenDatabaseCommand
        {
            get
            {
                if (openDatabaseCommand == null)
                {
                    openDatabaseCommand = new RelayCommand(GetTDLFromFile);

                }
                return openDatabaseCommand;
            }
        }

        public void SaveToFile(object parameter)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TDL));
            var fileName = "TDL.txt";
            var fileNameDialog = new InputDialog("Salut");

            if (fileNameDialog.ShowDialog() == true)
            {
                fileName = fileNameDialog.Answer;
            }

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, treeViewModel._tdl);
            }
        }

        public void GetTDLFromFile(object param)
        {
            TDL newTDL = new TDL();

            var fileNameDialog = new InputDialog("Salut");
            fileNameDialog.label.Content = "Choose File Name";
            var fileName = "TDL.txt";

            if (fileNameDialog.ShowDialog() == true)
            {
                fileName = fileNameDialog.Answer;
            }

            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TDL));
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    newTDL = (TDL)serializer.Deserialize(stream);
                    treeViewModel._tdl = newTDL;
                    ADD_TDLVM = new Add_TDLVM(treeViewModel._tdl);
                    treeViewModel.NotifyPropertyChanged(nameof(treeViewModel._tdl));
                }
            }
            else
            {
                MessageBox.Show("This database dosen't exist.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
