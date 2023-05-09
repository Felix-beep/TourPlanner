using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        public List<Tour> Items
        {
            get
            {
                return _tourList?.ListOfTours.ToList();
            }
        }

        public void SetTourList(DepictedTourList tourlist)
        {
            if (_tourList == null)
            {
                _tourList = tourlist;
            }
        }

    }
}
