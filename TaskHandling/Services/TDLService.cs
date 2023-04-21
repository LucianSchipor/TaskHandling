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
            tdl.TasksCollection.Add(task);
        }
        public void AddTDL(TDL newTDL)
        {
            tdl.TdlCollection.Add(newTDL);
        }

        public void RemoveTask(Task task) { 
            tdl.TasksCollection.Remove(task);
        }

        public void RemoveTDL(TDL tDL)
        {
            tdl.TdlCollection.Remove(tDL);
        }

    }
}
