using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
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
        private IBackendLogic _bl;

        private List<Tour> _listoftours = new();

        public event Action ListChanged;

        public List<Tour> ListOfTours {
            get
            {
                return _listoftours;
            }
            private set
            {
                _listoftours = value;
                ListChanged?.Invoke();
                OnPropertyChanged();
            }
        }

        public DepictedTourList(IBackendLogic bl)
        {
            _bl = bl;
            var updateToursTask = UpdateToursAsync();
            updateToursTask.Wait();
        }

        public async Task UpdateToursAsync()
        {
            ListOfTours = (await _bl.GetAllToursAsync()).ToList(); 
        }

        public void Refresh()
        {
            ListChanged?.Invoke();
        }

        public void SetTours(IEnumerable<Tour> tours) 
        {
            ListOfTours = tours.ToList(); 
        }

        public Tour GetTour(int ID)
        {
            return ListOfTours.First(t => t.ID == ID);
        }

        public TourLog GetTourLog(int tourID, int tourLogID)
        {
            Tour tour = ListOfTours.First(t => t.ID == tourID);
            TourLog tourLog = tour.logs.First(t => t.ID == tourLogID);
            return tourLog;
        }

        
    }
}
