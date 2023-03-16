using System;
using System.Collections.Generic;
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

        public SearchbarViewModel(EventHandler<string> OnSearchClicked)
        {
            ExecuteCommandGreet = new RelayCommand(param => SearchClicked?.Invoke(this, searchtext));
            SearchClicked += OnSearchClicked;
        }

        public ICommand ExecuteCommandGreet { get; }

        public event EventHandler<string> SearchClicked;
    }
}
