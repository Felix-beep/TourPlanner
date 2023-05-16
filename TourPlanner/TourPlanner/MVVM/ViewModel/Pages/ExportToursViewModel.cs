using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class ExportToursViewModel : ObservableObject
    {
        private DepictedTourList? _tourList;

        public IList SelectedItems { get; set;  }

        public List<Tour> Items
        {
            get
            {
                return _tourList?.ListOfTours.ToList();
            }
        }

        public ICommand ExportTours { get; }

        public ExportToursViewModel(DepictedTourList tourlist)
        {
            _tourList = tourlist;
            ExportTours = new RelayCommand(param => SubmitClicked?.Invoke(this, GetSelectedTourIds()));
        }

        public event EventHandler<List<Tour>> SubmitClicked;

        public List<Tour> GetSelectedTourIds()
        {
            List<Tour> TourIDs = new();

            foreach (var selectedItem in SelectedItems)
            {
                var selectedRow = (Tour)selectedItem;
                TourIDs.Add(selectedRow);
            }

            return TourIDs;
        }
    }
}
