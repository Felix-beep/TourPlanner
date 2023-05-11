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

        public List<Tour> Discover {
            get
            {
                return Discover;
            }
            set
            {
                Discover = value;
                OnPropertyChanged();
            }
        }

        public List<Tour> Popular {
            get
            {
                return Popular;
            } 
            set {
                Popular = value;
                OnPropertyChanged();
            } 
        }

        public HomeViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;
        }

        public void SetTourList(DepictedTourList tourlist)
        {
            
        }

    }
}
