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

        private List<Tour> _listoftours;

        public List<Tour> ListOfTours {
            get
            {
                return _listoftours;
            }
            private set
            {
                _listoftours = value;
                OnPropertyChanged();
            }
        }

        public DepictedTourList(IBackgroundLogic bl)
        {
            _bl = bl;
            ListOfTours = bl.GetAllTours().ToList(); 
        }

        public void UpadteTours()
        {
            ListOfTours = _bl.GetAllTours().ToList(); 
        }

        public void SetTours(IEnumerable<Tour> tours) 
        {
            ListOfTours = tours.ToList(); 
        }
    }
}
