using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }   
    }
}
