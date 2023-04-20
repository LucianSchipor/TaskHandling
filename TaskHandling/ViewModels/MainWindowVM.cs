using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaskHandling.Models;

namespace TaskHandling.ViewModels
{
    class MainWindowVM
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
    }
}
