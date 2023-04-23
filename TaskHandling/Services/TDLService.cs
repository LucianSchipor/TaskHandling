using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TaskHandling.Models;

namespace TaskHandling.Services
{
    class TDLService
    {
        private TDL tdl;
        public TDLService(TDL tdl)
        {
            this.tdl = tdl;
        }
        public void AddTask(Task task)
        {
            tdl.TasksCollection.Add(task);
        }
        public void AddTDL(TDL newTDL)
        {
            tdl.TdlCollection.Add(newTDL);
        }

        public void RemoveTask(Task task)
        {
            tdl.TasksCollection.Remove(task);
        }

        public void RemoveTDL(TDL tDL)
        {
            tdl.TdlCollection.Remove(tDL);
        }

        private void SwapTDLs(TDL first, TDL second)
        {
            var aux = new TDL();
            aux = first;
            first = second;
            second = aux;
        }

        public void MoveTDL(string mode)
        {
            if (mode == "up")
            {
                var index = this.tdl.parent.TdlCollection.IndexOf(this.tdl);
                if (index != 0)
                    SwapTDLs(this.tdl.parent.TdlCollection[index], this.tdl.parent.TdlCollection[index - 1]);
                else
                    MessageBox.Show("This TDL is the first.");
            }
        }
    }
}
