using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.MVVM.Model;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private String Title = "TourPlanner";

        private ViewObject _currentView;
        public ViewObject CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        

        // Behavior, UI event handlers
        public MainViewModel()
        {
            // CurrentView = ViewAssembly.HomeViewObject.Content;
            _currentView = ViewAssembly.HomeViewObject;

            /*addGreetingBarViewModel.GreetingButtonClicked += (_, greetingName) => AddGreeting(greetingName);

            ExecuteCommandExit = new RelayCommand(p => System.Environment.Exit(0));
            ExecuteCommandOpenTourPlanner = new RelayCommand(p => new Views.TourPlannerWindow().ShowDialog());*/
        }
    }
}
