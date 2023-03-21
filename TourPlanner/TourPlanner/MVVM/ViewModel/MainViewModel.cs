using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.MVVM.Model;

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

        public CreateRoutesViewModel CreateRoutesViewInstance;
        public ICommand SwapToCreateRoutes { get; }

        public BrowseRoutesViewModel BrowseRoutesViewInstance;
        public ICommand SwapToBrowseRoutes { get; }

        // Behavior, UI event handlers
        public MainViewModel()
        {
            SwapToHomeView = new RelayCommand(param => CurrentView = HomeViewInstance);
            SwapToCreateRoutes = new RelayCommand(param => CurrentView = CreateRoutesViewInstance);
            SwapToBrowseRoutes = new RelayCommand(param => CurrentView = BrowseRoutesViewInstance);

            /*addGreetingBarViewModel.GreetingButtonClicked += (_, greetingName) => AddGreeting(greetingName);

            ExecuteCommandExit = new RelayCommand(p => System.Environment.Exit(0));
            ExecuteCommandOpenTourPlanner = new RelayCommand(p => new Views.TourPlannerWindow().ShowDialog());*/
        }

        public void UpdateViews()
        {
            SearchbarInstance.SearchClicked += (_, searchtext) => Title = searchtext;
        }
    }
}
