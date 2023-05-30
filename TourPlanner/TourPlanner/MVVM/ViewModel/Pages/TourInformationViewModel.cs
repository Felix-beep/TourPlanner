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

        public TourInformationViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;


            CreateNewTourLog = new RelayCommand(param => CreateClicked?.Invoke());
            EditTourLogInformation = new RelayCommand<int>(param => EditClicked?.Invoke(this, param));
            DeleteTourLogInformation = new RelayCommand<int>(param => DeleteClicked?.Invoke(this, param));
        }

        public void OpenTour(int ID)
        {
            DisplayedTour = _tourList.ListOfTours.First(t => t.ID == ID);

        }

        public ICommand CreateNewTourLog { get; }
        public event Action CreateClicked;

        public ICommand EditTourLogInformation { get; }
        public event EventHandler<int> EditClicked;

        public ICommand DeleteTourLogInformation { get; }
        public event EventHandler<int> DeleteClicked;
    }
}
