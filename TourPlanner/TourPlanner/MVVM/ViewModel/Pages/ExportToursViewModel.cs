using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class ExportToursViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        public List<Tour> Items
        {
            get
            {
                return _tourList?.ListOfTours.ToList();
            }
        }

        public ExportToursViewModel(DepictedTourList tourlist)
        {
            if (_tourList == null)
            {
                _tourList = tourlist;
            }
        }
    }
}
