using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner.Core
{
    public class RelayCommand<T> : RelayCommand
    {
        public RelayCommand(Action<T> execute) : base(o => execute((T)o))
        {

        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute) : base(o => execute((T)o), o => canExecute((T)o))
        {

        }
    }
}
