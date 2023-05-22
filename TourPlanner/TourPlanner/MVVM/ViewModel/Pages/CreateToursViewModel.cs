using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class CreateToursViewModel : ObservableObject
    {
        private int? _tourID;
        public string DisplayedTourID { get { return (_tourID == null) ? "Creating New Tour" : "Editing Tour: " + _tourID.ToString(); } }

        private string _tourName;
        public String TourName 
        {
            get 
            { 
                return (_tourName == "") ? "Enter your tour name" : _tourName; 
            } 
            set 
            {
                value = value.Replace("Enter your tour name", string.Empty);
                _tourName = value; 
            } 
        }

        private string _description;
        public String Description
        {
            get
            {
                return (_description == "") ? "Enter a description" : _description;
            }
            set
            {
                value = value.Replace("Enter a description", string.Empty);
                _description = value;
            }
        }

        private string _from;
        public String From
        {
            get
            {
                return (_from == "") ? "Enter a starting point" : _from;
            }
            set
            {
                value = value.Replace("Enter a starting point", string.Empty);
                _from = value;
            }
        }

        private string _to;
        public String To
        {
            get
            {
                return (_to == "") ? "Enter a destination" : _to;
            }
            set
            {
                value = value.Replace("Enter a destination", string.Empty);
                _to = value;
            }
        }

        private string _transport;
        public String Transport
        {
            get
            {
                return (_transport == "") ? "Enter a transport type" : _transport;
            }
            set
            {
                value = value.Replace("Enter a transport type", string.Empty);
                _transport = value;
            }
        }

        public CreateToursViewModel() {
            FillWithEmpty();
            SubmitForm = new RelayCommand( param => {
                if(_tourID == null)
                {
                    NewTourSubmitted?.Invoke(this, ConvertToTour());
                } 
                else
                {
                    OldTourSubmitted?.Invoke(this, ConvertToTour());
                }
                });
        }

        public ICommand SubmitForm { get; }

        public event EventHandler<Tour> NewTourSubmitted;
        public event EventHandler<Tour> OldTourSubmitted;

        public void OpenTour(Tour? tour)
        {
            if(tour == null) { FillWithEmpty(); }
            else
            {
                _tourID = tour.ID;
                _tourName = tour.name;
                _description = tour.description;
                _from = tour.from;
                _to = tour.to;
                _transport = tour.transportType;
            }
        }

        private void FillWithEmpty()
        {
            _tourID = null;
            _tourName = "";
            _description = "";
            _from = "";
            _to = "";
            _transport = "";
        }

        private Tour ConvertToTour()
        {
            int id;
            if(_tourID == null)
            {
                id = 0;
            } else
            {
                id = (int)_tourID;
            }

            return new Tour()
            {
                ID = id,
                name = _tourName,
                description = _description,
                from = _from,
                to = _to,
                transportType = _transport,
            };
        }
    }
}
