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
        public Task selectedTask;
        public TDL ROOT;
        public TreeViewModel()
        {
            _tdl = new TDL();
            selectedItem = new TDL();
            selectedItem.Name = "Unknown";
            selectedTask = new Task();
            selectedTask.taskDescription = "Hi brother";
        }

        public TDL selectedItem;
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
        public string currentDatabase { get; set; }
        public Add_TDLVM ADD_TDLVM { get; set; }
        TDLService tdlService;
        public TreeViewModel treeViewModel { get; set; }
        public MainWindowVM()
        {
            treeViewModel = new TreeViewModel();
            tdlService = new TDLService(treeViewModel._tdl);
            ADD_TDLVM = new Add_TDLVM(treeViewModel._tdl, treeViewModel._tdl.tdlservice);
            currentDatabase = "Unknown";
        }

        public void SaveAfterAction()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(TDL));
            using (FileStream stream = new FileStream(currentDatabase, FileMode.Create))
            {
                serializer.Serialize(stream, treeViewModel._tdl);
            }
        }

        public void Add_Root_TDL(object param)
        {
            var addWindow = new Add_TDL();
            addWindow.DataContext = ADD_TDLVM;
            addWindow.Show();
        }

        private void selectItem(TDL tdl)
        {
            treeViewModel.SelectedItem = tdl;
        }

        private ICommand selectTaskCommand;
        public ICommand SelectTaskCommand
        {
            get
            {
                if (selectTaskCommand == null)
                {
                    selectTaskCommand = new RelayCommandN<Task>(SelectTask);
                }
                return selectTaskCommand;
            }
        }

        private void SelectTask(Task task)
        {
            treeViewModel.selectedTask = task;
        }

        private ICommand selectItemCommand;
        public ICommand SelectItemCommand
        {
            get
            {
                if (selectItemCommand == null)
                {
                    selectItemCommand = new RelayCommandN<TDL>(selectItem);
                }
                return selectItemCommand;
            }
        }

        private ICommand addSubTDLCommand;
        public ICommand AddSubTDLCommand
        {
            get
            {
                if (addSubTDLCommand == null)
                {
                    addSubTDLCommand = new RelayCommand(AddSubTDL);
                }
                return addSubTDLCommand;
            }
        }

        public void AddSubTDL(object param)
        {
            var selectedItemTdlService = new TDLService(treeViewModel.SelectedItem);
            selectedItemTdlService.currentFileName = currentDatabase;
            var ADD_TDLVM = new Add_TDLVM(treeViewModel.SelectedItem, selectedItemTdlService);
            var window = new Add_TDL();
            window.DataContext = ADD_TDLVM;
            window.Show();
        }

        private ICommand addTaskToTDLCommand;
        public ICommand AddTaskToTDLCommand
        {
            get
            {
                if (addTaskToTDLCommand == null)
                {
                    addTaskToTDLCommand = new RelayCommandN<TDL>(AddTaskToTDL);
                }
                return addTaskToTDLCommand;
            }
        }

        private ICommand editTDLCommand;
        public ICommand EditTDLCommand
        {
            get
            {
                if (editTDLCommand == null)
                {
                    editTDLCommand = new RelayCommand(EditTDL);
                }
                return editTDLCommand;
            }
        }

        public void EditTDL(object param)
        {
            var window = new Edit();
            window.DataContext = new EditVM(treeViewModel.selectedItem, treeViewModel._tdl);
            window.Show();

        }

        private void AddTaskToTDL(object param)
        {
            if (treeViewModel.SelectedItem.Name == "Unknown")
            {
                MessageBox.Show("Niciun tdl selectat.");
            }
            else
            {
                var window = new Add_TASK();
                window.DataContext = new Add_TaskVM(treeViewModel._tdl, treeViewModel.selectedItem.TasksCollection);
                window.Show();
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
                    treeViewModel._tdl.tdlservice.currentFileName = fileName;

                    ADD_TDLVM = new Add_TDLVM(treeViewModel._tdl, treeViewModel._tdl.tdlservice);

                    currentDatabase = fileName;
                    treeViewModel.NotifyPropertyChanged(nameof(treeViewModel._tdl));
                    this.OnPropertyChanged(nameof(currentDatabase));
                }
            }
            else
            {
                if (fileName != "Unknown")
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

        private ICommand deleteTDLCommand;
        public ICommand DeleteTDLCommand
        {
            get
            {
                if (deleteTDLCommand == null)
                {
                    deleteTDLCommand = new RelayCommand(DeleteTDL);

                }
                return deleteTDLCommand;
            }
        }

        public void DeleteTDL(object param)
        {
            treeViewModel._tdl.TdlCollection.Remove(treeViewModel.SelectedItem);
            treeViewModel._tdl.NotifyPropertyChanged(nameof(treeViewModel._tdl));
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
            var fileName = " ";
            if (fileNameDialog.ShowDialog() == true)
            {
                fileName = fileNameDialog.Answer;
            }

            if (fileName != " ")
            {

                if (File.Exists(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TDL));
                    using (FileStream stream = new FileStream(fileName, FileMode.Open))
                    {
                        newTDL = (TDL)serializer.Deserialize(stream);
                        treeViewModel._tdl = newTDL;
                        treeViewModel._tdl.tdlservice = new TDLService(newTDL);
                        treeViewModel._tdl.tdlservice.currentFileName = fileName;
                        ADD_TDLVM = new Add_TDLVM(treeViewModel._tdl, treeViewModel._tdl.tdlservice);
                        treeViewModel.NotifyPropertyChanged(nameof(treeViewModel._tdl));
                        currentDatabase = fileName;
                        this.OnPropertyChanged(nameof(currentDatabase));
                    }
                }
                else
                {
                    MessageBox.Show("This database dosen't exist.");
                }
            }
        }

        private ICommand editTaskCommand;
        public ICommand EditTaskCommand
        {
            get
            {
                if (editTaskCommand == null)
                {
                    editTaskCommand = new RelayCommand(EditTask);

                }
                return editTaskCommand;
            }
        }
        public void EditTask(object param)
        {
            var EditTask = new Add_TASK();
            EditTask.actionButton.Content = "Edit";
            var context = new Add_TaskVM(treeViewModel.SelectedItem, treeViewModel.selectedItem.TasksCollection);
            EditTask.DataContext = context;
            EditTask.actionButton.Command = editTaskCommand;
            EditTask.Show();
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
            var window = new FindTask();
            var context = new FindTaskVM(treeViewModel._tdl);
            window.DataContext = context;
            window.Show();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
