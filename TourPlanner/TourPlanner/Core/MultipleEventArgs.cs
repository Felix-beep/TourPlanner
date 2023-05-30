using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Core
{
    public class MultipleEventArgs : EventArgs
    {
        public object[] Args { get; set; }
        public MultipleEventArgs(params object[] args)
        {
            Args = args;
        }
    }
}
