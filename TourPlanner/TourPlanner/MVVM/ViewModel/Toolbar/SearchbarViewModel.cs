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

        private bool _isonline;

        public bool IsOnline
        {
            get { return _isonline; }
            set
            {
                _isonline = value;
                OnPropertyChanged();
            }
        } 

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
            ExecuteCommandSwapMode = new RelayCommand(param => SwapClicked?.Invoke());
            IsOnline = false;
        }

        public ICommand ExecuteCommandSearch { get; }

        public event EventHandler<string> SearchClicked;
        public ICommand ExecuteCommandSwapMode { get; }

        public event Action SwapClicked;

    }
}
