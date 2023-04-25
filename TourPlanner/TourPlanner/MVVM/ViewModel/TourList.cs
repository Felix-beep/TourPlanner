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
        IBackgroundLogic _bl;
        public IEnumerable<Tour> ListOfTours { get; private set; }

        public TourList(IBackgroundLogic BL)
        {
            _bl = BL;
            ListOfTours = BL.GetAllTours();
        }

        public void UpadteTours()
        {
            ListOfTours = _bl.GetAllTours();
        }
    }
}
