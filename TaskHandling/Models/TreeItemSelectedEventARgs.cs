using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHandling.Models
{
    public class TreeItemSelectedEventARgs: EventArgs
    {
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public string taskType { get; set; }

        public string status;

        public string priority;

        public DateTime deadLine { get; set; }
        public DateTime taskDoneTime { get; set; }
    }
}
