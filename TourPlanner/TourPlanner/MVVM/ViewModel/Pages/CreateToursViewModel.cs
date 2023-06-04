using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class CreateToursViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private InputErrorDic _errorDictionary;
        public bool CanSubmit => !(_errorDictionary.HasErrors || _tourName == "");
        public bool HasErrors => _errorDictionary.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private int? _tourID;
        public string DisplayedTourID { get { return (_tourID == null) ? "Creating New Tour" : "Editing Tour: " + _tourID.ToString(); } }

        private string _tourName;

        private string _emptyTourName = "Enter your tour name";
        public String TourName 
        {
            get 
            { 
                return (_tourName == "") ? _emptyTourName : _tourName; 
            } 
            set 
            {
                value = value.Replace(_emptyTourName, string.Empty);
                _tourName = value;

                _errorDictionary.ClearErrors(nameof(TourName));
                if(_tourName.Length > 25)
                {
                    _errorDictionary.AddError(nameof(TourName), "Name is too long. It cannot exceed 25 characters.");
                }
                if (_tourName.Length < 6)
                {
                    _errorDictionary.AddError(nameof(TourName), "Name is too short. It must exceed 5 characters.");
                }
            } 
        }

        private string _description;

        private string _emptyDescription = "Enter a description";
        public String Description
        {
            get
            {
                return (_description == "") ? _emptyDescription : _description;
            }
            set
            {
                value = value.Replace(_emptyDescription, string.Empty);
                _description = value;
            }
        }

        private string _from;
        private string _emptyFrom = "Enter a starting point";
        public String From
        {
            get
            {
                return (_from == "") ? _emptyFrom : _from;
            }
            set
            {
                value = value.Replace(_emptyFrom, string.Empty);
                _from = value;

                _errorDictionary.ClearErrors(nameof(From));
                if (_from.Length > 25)
                {
                    _errorDictionary.AddError(nameof(From), "Starting Location is too long. It cannot exceed 25 characters.");
                }
                if (_from.Length < 6)
                {
                    _errorDictionary.AddError(nameof(From), "Starting Location is too short. It must exceed 5 characters.");
                }
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

                _errorDictionary.ClearErrors(nameof(To));
                if (_to.Length > 25)
                {
                    _errorDictionary.AddError(nameof(To), "Ending Location is too long. It cannot exceed 25 characters.");
                }
                if (_to.Length < 6)
                {
                    _errorDictionary.AddError(nameof(To), "Ending Location is too short. It must exceed 5 characters.");
                }
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

                _errorDictionary.ClearErrors(nameof(Transport));
                if (!(_transport == "fastest" || _transport == "shortest" || _transport == "pedestrian" || _transport == "bicycle"))
                {
                    _errorDictionary.AddError(nameof(Transport), "Valid transport types are: fastest, shortest, pedestrian, bicycle.");
                }
            }
        }
        public CreateToursViewModel() {
            _errorDictionary = new InputErrorDic();
            _errorDictionary.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
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

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanSubmit));
            ErrorsChanged?.Invoke(this, e);
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
            TourName = "";
            Description = "";
            From = "";
            To = "";
            Transport = "";
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
        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorDictionary.GetErrors(propertyName);
        }
    }
}
