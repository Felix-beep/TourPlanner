using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class TourList
    {
        private IBackgroundLogic _bl;
        public IEnumerable<Tour> ListOfTours { get; private set; }

        public TourList(IBackgroundLogic bl)
        {
            _bl = bl;
            ListOfTours = bl.GetAllTours();
        }

        public void UpadteTours()
        {
            ListOfTours = _bl.GetAllTours();
        }
    }
}
