using System;

namespace TaskHandling.Models
{
    public class Task
    {
        public Task()
        {
            this.taskName = "New Task";
            this.taskDescription = "No description";
            this.taskType = "Type Undefined";
            this.taskStatus = "Just Created";
            this.taskPriority = "Priority Undefined";
            taskDoneTime = DateTime.Now;
            deadLine = DateTime.Now;
        }

        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public string taskType { get; set; }

        public string taskStatus { get; set; }

        public string taskPriority { get; set; }

        public DateTime deadLine { get; set; }
        public DateTime taskDoneTime { get; set; }


    }
}
