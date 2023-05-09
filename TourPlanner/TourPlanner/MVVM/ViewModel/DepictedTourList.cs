using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class DepictedTourList : ObservableObject
    {
        private IBackgroundLogic _bl;

        public IEnumerable<Tour> ListOfTours {
            get;
            private set;
        }

        public DepictedTourList(IBackgroundLogic bl)
        {
            _bl = bl;
            ListOfTours = bl.GetAllTours();
        }

        public void UpadteTours()
        {
            ListOfTours = _bl.GetAllTours();
        }

        public void SetTours(IEnumerable<Tour> tours) 
        {
            ListOfTours = tours;
        }
    }
}
