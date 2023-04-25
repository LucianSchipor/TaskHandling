using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskHandling.Commands;
using TaskHandling.Models;
using TaskHandling.Views;

namespace TaskHandling.ViewModels
{
    public class EditVM
    {
        public TDL tdl;
        public TDL principalTDL;
        public EditVM(TDL tdl, TDL tdl2)
        {
            this.tdl = tdl;
            this.principalTDL = tdl2;
        }

        public EditVM()
        {

        }

        private ICommand editNameCommand;
        public ICommand EditNameCommand
        {
            get
            {
                if (editNameCommand == null)
                {
                    editNameCommand = new RelayCommand(EditName);
                }
                return editNameCommand;
            }
        }

        public void EditName(object param)
        {
            var dialog = new InputDialog("");

            if (dialog.ShowDialog() == true)
            {
                this.tdl.Name = dialog.Answer;
            }
            //ii trebuie parinte sau cv
            principalTDL.tdlservice.SaveToFile(null);
        }


        private ICommand editImgPathCommand;
        public ICommand EditImgPathCommand
        {
            get
            {
                if (editImgPathCommand == null)
                {
                    editImgPathCommand = new RelayCommand(EditImgPath);
                }
                return editImgPathCommand;
            }
        }

        private void EditImgPath(object param)
        {
            var browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == true)
            {
                this.tdl.Image = browseDialog.FileName;
            }
            principalTDL.tdlservice.SaveToFile(null);
        }
    }
}
