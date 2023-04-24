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
            selectedItem = new TDL();
            selectedItem.Name = "Unknown";
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

         public RoutedEventHandler MyDoubleClickCommand
        {
            get
            {
                return new RoutedEventHandler((sender, e) =>
                {
                    var element = sender as FrameworkElement;
                    if (element != null)
                    {
                        var item = element.DataContext;
                        this.selectedItem = item as TDL;
                        NotifyPropertyChanged(nameof(selectedItem));
                    }
                });
            }
        }

    }

    public class MainWindowVM : INotifyPropertyChanged
    {
        public int tasksDueToday;
        public int tasksDueTomorrow;
        public int tasksOverDue;
        public int tasksDone;
        public int tasksToBeDone;
        //le voi lega cu butoanele de add de tasks

        public string currentDatabase { get ; set; }
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
            currentDatabase = "Undefined";
        }

        public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            addWindow.DataContext = ADD_TDLVM;
            MessageBox.Show("BV");
            addWindow.Show();
            
        }

        private void selectItem(TDL tdl)
        {
            treeViewModel.SelectedItem = tdl;
        }

        private ICommand selectItemCommand;
        public ICommand SelectItemCommand
        {
            get
            {
                if(selectItemCommand == null)
                {
                    selectItemCommand = new RelayCommandN<TDL>(selectItem);
                }
                return selectItemCommand;
            }
            set
            {

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
                    archiveDatabaseCommand = new RelayCommand(tdlService.SaveToFile);
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

        public void CreateToFile(object sender)
        {
            var fileName = "Unknown";
            var fileNameDialog = new InputDialog("Salut");
            fileNameDialog.label.Content = "Choose File Name";
            if (fileNameDialog.ShowDialog() == true)
            {
                fileName = fileNameDialog.Answer;
            }

            if (!File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TDL));
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    TDL newTDL = new TDL();
                    serializer.Serialize(stream, newTDL);
                    treeViewModel._tdl = newTDL;
                    ADD_TDLVM._tdlService.currentFileName = fileName;
                    currentDatabase = fileName;
                    this.OnPropertyChanged(nameof(currentDatabase));
                }
            }
            else
            {
                if (fileName != " ")
                    MessageBox.Show("Database with this name already exist.");
            }
        }

        private ICommand createDatabaseCommand;
        public ICommand CreateDatabaseCommand
        {
            get
            {
                if (createDatabaseCommand == null)
                {
                    createDatabaseCommand = new RelayCommand(CreateToFile);

                }
                return createDatabaseCommand;
            }
        }

        public void SaveToFile(object parameter)
        {

            var fileName = "TDL.txt";
            var fileNameDialog = new InputDialog("Salut");
            fileNameDialog.label.Content = "Choose File Name";
            if (fileNameDialog.ShowDialog() == true)
            {
                fileName = fileNameDialog.Answer;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(TDL));
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
                    tdlService.currentFileName = fileName;
                    currentDatabase = fileName;
                    this.OnPropertyChanged(nameof(currentDatabase));
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
