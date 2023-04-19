using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            tdl.tasksColletion.Add(task);
        }
        public void AddTDL()
        {
            tdl.tdlCollection.Add(tdl);
        }

        public void RemoveTask(Task task) { 
            tdl.tasksColletion.Remove(task);
        }

        public void RemoveTDL(TDL tDL)
        {
            tdl.tdlCollection.Remove(tDL);
        }

    }
}
