using System;
using System.Collections.Generic;
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
        private IBackgroundLogic _bl;

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

        public DepictedTourList(IBackgroundLogic bl)
        {
            _bl = bl;
            ListOfTours = bl.GetAllTours().ToList();
        }

        public void UpadteTours()
        {
            ListOfTours = _bl.GetAllTours().ToList(); 
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

        
    }
}
