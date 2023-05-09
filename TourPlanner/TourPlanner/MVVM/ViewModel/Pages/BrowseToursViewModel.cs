using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;
using System.Windows.Input;
using System.Windows.Controls;

namespace TourPlanner.MVVM.ViewModel
{
    public class BrowseToursViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        public List<Tour> Items {
            get {
                return _tourList?.ListOfTours.ToList();
            }
        }


        public ICommand OpenTourInformation { get; }

        public BrowseToursViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;
            OpenTourInformation = new RelayCommand<int>(newRelayCommand);
        }

        private void newRelayCommand(int ID)
        {
            TourClicked?.Invoke(this, ID);
        }



        public event EventHandler<int> TourClicked;
    }
}
