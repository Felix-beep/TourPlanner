using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public String _title = "TourPlanner";
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private object _currentHotbar;
        public object CurrentHotbar
        {
            get { return _currentHotbar; }
            set
            {
                _currentHotbar = value;
                OnPropertyChanged();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public SearchbarViewModel SearchbarInstance;
        
        public HomeViewModel HomeViewInstance;
        public ICommand SwapToHomeView { get; }

        public CreateToursViewModel CreateToursViewInstance;
        public ICommand SwapToCreateTours { get; }

        public BrowseToursViewModel BrowseToursViewInstance;
        public ICommand SwapToBrowseTours { get; }

        public ImportToursViewModel ImportToursViewInstance;
        public ICommand SwapToImportTours { get; }

        public ExportToursViewModel ExportToursViewInstance;
        public ICommand SwapToExportTours { get; }

        // Behavior, UI event handlers
        public MainViewModel()
        {
            SwapToHomeView = new RelayCommand(param => CurrentView = HomeViewInstance);
            SwapToCreateTours = new RelayCommand(param => CurrentView = CreateToursViewInstance);
            SwapToBrowseTours = new RelayCommand(param => CurrentView = BrowseToursViewInstance);
            SwapToImportTours = new RelayCommand(param => CurrentView = ImportToursViewInstance);
            SwapToExportTours = new RelayCommand(param => CurrentView = ExportToursViewInstance);
        }

        public void UpdateViews()
        {
            SearchbarInstance.SearchClicked += (_, searchtext) => Title = searchtext;
        }
    }
}
