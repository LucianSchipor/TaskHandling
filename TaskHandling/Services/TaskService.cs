using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHandling.Services
{
    class TaskService
    {
        private Task task;
        public TaskService(Task task) 
        {
            this.task = task;
        }

        
        public void AddTask(Task task) { 
        }

        public void RemoveTask(Task task)
        {
        }
    }
}
