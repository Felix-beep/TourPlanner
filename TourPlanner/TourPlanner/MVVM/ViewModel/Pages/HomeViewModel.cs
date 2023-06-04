using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BL;
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
                return _discover.OrderBy(x => RandomNumberGenerator.GetInt32(100)).ToList();
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
                return _popular.OrderByDescending(TourAttributeComputer.GetPopularity).ToList();
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
            OpenTourInformation = new RelayCommand<int>(param => ViewClicked?.Invoke(this, param));
        }

        public void UpdateTours()
        {
            Discover = _tourList.ListOfTours;
            Popular = _tourList.ListOfTours;
        }


        public ICommand OpenTourInformation { get; }
        public event EventHandler<int> ViewClicked;

    }
}
