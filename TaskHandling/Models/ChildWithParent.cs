using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHandling.Models
{
    public class ChildWithParent<T>
    {
        public TDL Child { get; set; }
        public TDL Parent { get; set; }
        public ChildWithParent(TDL Child, TDL Parent) 
        {
            this.Child = Child;
            this.Parent = Parent;
        }
        public ChildWithParent()
        {
            Child= new TDL();
            Parent = new TDL();

        }
    }
}
