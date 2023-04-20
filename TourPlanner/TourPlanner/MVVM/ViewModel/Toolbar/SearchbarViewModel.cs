using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Core;

namespace TourPlanner.MVVM.ViewModel
{
    public class SearchbarViewModel : ObservableObject
    {
        private String searchtext = "";

        public String SearchText { 
            get { return searchtext; }
            set
            {
                searchtext = value;
                OnPropertyChanged();
            }
        }

        public SearchbarViewModel()
        {
            ExecuteCommandSearch = new RelayCommand(param => SearchClicked?.Invoke(this, searchtext));
        }

        public ICommand ExecuteCommandSearch { get; }

        public event EventHandler<string> SearchClicked;

    }
}
