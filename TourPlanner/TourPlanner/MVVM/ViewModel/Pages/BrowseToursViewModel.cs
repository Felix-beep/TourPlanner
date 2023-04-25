using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class BrowseToursViewModel : ObservableObject
    {
        private TourList TourList;

        public List<Tour> Items {
            get {
                return TourList?.ListOfTours.ToList();
            }
        }

        public void SetTourList(TourList tourlist)
        {
            if(TourList == null)
            {
                TourList = tourlist;
            }
        }
    }
}
