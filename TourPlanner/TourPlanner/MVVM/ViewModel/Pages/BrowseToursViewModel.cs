using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;
using System.Windows.Input;
using System.Windows.Controls;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace TourPlanner.MVVM.ViewModel
{
    public class BrowseToursViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        public List<Tour> Items {
            get {
                return _tourList?.ListOfTours.ToList();
            }
        }
        public ICommand CreateNewTour { get; }
        public event Action NewTourClicked;

        public ICommand OpenTourInformation { get; }
        public event EventHandler<int> ViewClicked;

        public ICommand EditTourInformation { get; }
        public event EventHandler<int> EditClicked;

        public ICommand DeleteTourInformation { get; }
        public event EventHandler<int> DeleteClicked;

        public BrowseToursViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;

            CreateNewTour = new RelayCommand(param => NewTourClicked?.Invoke());

            OpenTourInformation = new RelayCommand<int>(param => ViewClicked?.Invoke(this, param));
            EditTourInformation = new RelayCommand<int>(param => EditClicked?.Invoke(this, param));
            DeleteTourInformation = new RelayCommand<int>(param => DeleteClicked?.Invoke(this, param));
        }
    }
}
