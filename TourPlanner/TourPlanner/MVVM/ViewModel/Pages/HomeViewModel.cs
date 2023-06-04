using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        private List<Tour> _discover;
        public List<Tour> Discover {
            get
            {
                return _popular.OrderBy(x => RandomNumberGenerator.GetInt32(100)).ToList();
            }
            set
            {
                _discover = value;
                OnPropertyChanged();
            }
        }

        private List<Tour> _popular;

        public List<Tour> Popular {
            get
            {
                return _popular.OrderBy(x => x.logs.Count).ToList();
            } 
            set {
                _popular = value;
                OnPropertyChanged();
            } 
        }

        public HomeViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;
            _tourList.ListChanged += UpdateTours;
        }

        public void UpdateTours()
        {
            Discover = _tourList.ListOfTours;
            Popular = _tourList.ListOfTours;
        }

    }
}
