using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using TaskHandling.Models;
using TaskHandling.Commands;
using System.ComponentModel;
using TaskHandling.Views;
using TaskHandling.Services;
using System.Collections.ObjectModel;

namespace TaskHandling.ViewModels
{
    public class Add_TDLVM : INotifyPropertyChanged
    {
        public TDLService _tdlService;
        public TDL _tdl;

        private string imgPath;
        public string IMGPath
        {
            get
            {
                return imgPath;
            }
            set
            {
                imgPath = value;
                NotifyPropertyChanged(nameof(imgPath));
            }
        }
        private string tdlName;
        public string TDLName
        {
            get
            {
                return tdlName;
            }
            set
            {
                tdlName = value;
                NotifyPropertyChanged(nameof(tdlName));
            }
        }
        private TDL tdlparent;
        public TDL TDLParent
        {
            get
            {
                return tdlparent;
            }
            set
            {
                tdlparent = value;
                NotifyPropertyChanged(nameof(tdlparent));
            }
        }

        public Add_TDLVM(TDL tdlList, TDLService tDLService)
        {
            _tdl = tdlList;
            _tdlService = tDLService;
        }


        public Add_TDLVM()
        {

        }

        private void BrowseImage(object param)
        {
            var browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == true)
            {
                IMGPath = browseDialog.FileName;
            }
        }

        private void SetName(object param)
        {
            var nameDialog = new InputDialog("Salut");
            if (nameDialog.ShowDialog() == true)
            {
                TDLName = nameDialog.Answer;
            }
        }

        private void Add(object param)
        {
            TDL newTDL = new TDL();
            newTDL.Name = TDLName;
            newTDL.Image = IMGPath;
            
            if (newTDL.Name != null && newTDL.Image != null)
            {
                _tdlService.AddTDL(newTDL);
                _tdlService.SaveToFile(null);
                MessageBox.Show("To Do List" + TDLName + "a fost adaugat in lista!.");
            }
            else
            {
                if (newTDL.Name == null)
                {
                    MessageBox.Show("Nu ai setat niciun nume.");
                }
                else
                {
                    MessageBox.Show("Nu ai setat nicio imagine");
                }
            }
        }

        private ICommand browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    browseCommand = new RelayCommand(BrowseImage);
                }
                return browseCommand;
            }
        }

        private ICommand setNameCommand;
        public ICommand SetNameCommand
        {
            get
            {
                if (setNameCommand == null)
                {
                    setNameCommand = new RelayCommand(SetName);
                }
                return setNameCommand;
            }
        }

        private ICommand addTDLCommand;
        public ICommand AddTDLCommand
        {
            get
            {
                if (addTDLCommand == null)
                {
                    addTDLCommand = new RelayCommand(Add);
                }
                return addTDLCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

