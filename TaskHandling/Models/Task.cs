using System;

namespace TaskHandling.Models
{
    public class Task
    {
        public Task()
        {
            this.taskName = "New Task";
            this.taskDescription = "New Task";
            this.taskType = "New Task";
            this.status = "Created";
            this.priority = "Undefined";
        }

        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public string taskType { get; set; }

        public string status;

        public string priority;

        public DateTime deadLine { get; set; }
        public DateTime taskDoneTime { get; set; }


    }
}
