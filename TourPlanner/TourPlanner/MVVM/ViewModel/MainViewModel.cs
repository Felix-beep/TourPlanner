using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TourPlanner.Core;
using TourPlanner.MVVM.Model;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public String Title = "TourPlanner";

        public ViewModelObject HomeViewObject;

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

        // Behavior, UI event handlers
        public MainViewModel()
        {
            /*addGreetingBarViewModel.GreetingButtonClicked += (_, greetingName) => AddGreeting(greetingName);

            ExecuteCommandExit = new RelayCommand(p => System.Environment.Exit(0));
            ExecuteCommandOpenTourPlanner = new RelayCommand(p => new Views.TourPlannerWindow().ShowDialog());*/
        }

        public void UpdateViews()
        {
            ((SearchbarViewModel)HomeViewObject.Hotbar).SearchClicked += (_, searchtext) => Title = searchtext;
        }
    }
}
