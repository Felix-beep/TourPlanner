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
        }


        public void OpenTour(int ID)
        {
            DisplayedTour = _tourList.ListOfTours.First(t => t.ID == ID);
        }
    }
}
