using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;
using System.Windows.Input;

namespace TourPlanner.MVVM.ViewModel
{
    public class TourInformationViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;
        public Tour DisplayedTour { get; set; }

        public TourInformationViewModel(DepictedTourList tourList)
        {
            _tourList = tourList;


            CreateNewTourLog = new RelayCommand(parameter => CreateClicked?.Invoke());
            EditTourLogInformation = new RelayCommand<int>(parameter => EditClicked?.Invoke(this, new MultipleEventArgs(DisplayedTour.ID, parameter)));
            DeleteTourLogInformation = new RelayCommand<int>(parameter => DeleteClicked?.Invoke(this, new MultipleEventArgs(DisplayedTour.ID, parameter)));
        }

        public void OpenTour(int ID)
        {
            DisplayedTour = _tourList.ListOfTours.First(t => t.ID == ID);

        }

        public ICommand CreateNewTourLog { get; }
        public event Action CreateClicked;

        public ICommand EditTourLogInformation { get; }
        public event EventHandler<MultipleEventArgs> EditClicked;

        public ICommand DeleteTourLogInformation { get; }
        public event EventHandler<MultipleEventArgs> DeleteClicked;
    }
}
