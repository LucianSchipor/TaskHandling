﻿using Microsoft.Win32;
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

namespace TaskHandling.ViewModels
{
    public class Add_TDLVM : INotifyPropertyChanged
    {
        public TDL tdl;
        private TDLService tdlService;

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

        public Add_TDLVM()
        {
            tdl = new TDL();
                //trebuie ca TDL sa nu fie initializat, ci sa ia context-ul dinainte
            tdlService = new TDLService(tdl);
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
            if(nameDialog.ShowDialog() == true)
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
                tdlService.AddTDL(newTDL);
                MessageBox.Show("To Do List" + TDLName + "a fost adaugat in lista!.");
                tdl.NotifyPropertyChanged("TDLCollection");
            }
            else
            {
                if(newTDL.Name == null)
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
                if(browseCommand == null)
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

