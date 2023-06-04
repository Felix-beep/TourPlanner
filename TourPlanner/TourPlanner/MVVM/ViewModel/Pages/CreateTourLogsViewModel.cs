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
    public class CreateTourLogsViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private InputErrorDic _errorDictionary;
        public bool CanSubmit => !(_errorDictionary.HasErrors);
        public bool HasErrors => _errorDictionary.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private int _tourID;
        private int? _tourLogID;
        public string DisplayedTourLogID { get { return (_tourLogID == null) ? "Creating New Tour" : "Editing Tour: " + _tourLogID.ToString(); } }

        private string _tourComment;

        private string _emptyTourComment = "Enter your comment";
        public String TourComment
        {
            get
            {
                return (_tourComment == "") ? _emptyTourComment : _tourComment;
            }
            set
            {
                value = value.Replace(_emptyTourComment, string.Empty);
                _tourComment = value;


            }
        }

        private string _date;

        private string _emptyDate = "Enter the date on which you did the tour";
        public String Date
        {
            get
            {
                return (_date == "") ? _emptyDate : _date;
            }
            set
            {
                value = value.Replace(_emptyDate, string.Empty);
                _date = value;

                _errorDictionary.ClearErrors(nameof(Date));
                DateTime f;
                if (!DateTime.TryParse(_date, out f) || _date == "")
                {
                    _errorDictionary.AddError(nameof(Date), "Date has to be in the format yyyy-mm-dd");
                }
            }
        }

        private string _difficulty;

        private string _emptyDifficulty = "Enter estimated difficulty 1-5";
        public String Difficulty
        {
            get
            {
                return (_difficulty == "") ? _emptyDifficulty : _difficulty;
            }
            set
            {
                value = value.Replace(_emptyDifficulty, string.Empty);
                _difficulty = value;

                _errorDictionary.ClearErrors(nameof(Difficulty));
                float f;
                if (!float.TryParse(_difficulty, out f))
                {
                    _errorDictionary.AddError(nameof(Difficulty), "Difficulty may only consists of a number.");
                    return;
                }
                if (float.Parse(_difficulty) < 1 || float.Parse(_difficulty) > 5)
                {
                    _errorDictionary.AddError(nameof(Difficulty), "Difficulty has to be a number between 1 and 5.");
                }
            }
        }

        private string _totalTime;
        private string _emptyTotalTime = "Enter time it took you to complete the route";
        public String TotalTime
        {
            get
            {
                return (_totalTime == "") ? _emptyTotalTime : _totalTime;
            }
            set
            {
                value = value.Replace(_emptyTotalTime, string.Empty);
                _totalTime = value;

                _errorDictionary.ClearErrors(nameof(TotalTime));
                TimeSpan f;
                if (!TimeSpan.TryParse(_totalTime, out f) || _totalTime == "")
                {
                    _errorDictionary.AddError(nameof(TotalTime), "Time has to be in the format hh:mm:ss.");
                }
            }
        }

        private string _rating;
        private string _emptyRating = "Enter rating 1-5";
        public String Rating
        {
            get
            {
                return (_rating == "") ? _emptyRating : _rating;
            }
            set
            {
                value = value.Replace(_emptyRating, string.Empty);
                _rating = value;

                _errorDictionary.ClearErrors(nameof(Rating));
                float f;
                if (!float.TryParse(_rating, out f))
                {
                    _errorDictionary.AddError(nameof(Rating), "Difficulty may only consists of a number.");
                    return;
                }
                if (float.Parse(_rating) < 1 || float.Parse(_rating) > 5)
                {
                    _errorDictionary.AddError(nameof(Rating), "Rating has to be a number between 1 and 5.");
                }
            }
        }

        public CreateTourLogsViewModel()
        {
            _errorDictionary = new InputErrorDic();
            _errorDictionary.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            FillWithEmpty();
            SubmitForm = new RelayCommand(param => {
                if (_tourLogID == null)
                {
                    NewTourSubmitted?.Invoke(this, new MultipleEventArgs(_tourID, ConvertToTour()));
                }
                else
                {
                    OldTourSubmitted?.Invoke(this, new MultipleEventArgs(_tourID, ConvertToTour()));
                }
            });
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanSubmit));
            ErrorsChanged?.Invoke(this, e);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorDictionary.GetErrors(propertyName);
        }

        public ICommand SubmitForm { get; }

        public event EventHandler<MultipleEventArgs> NewTourSubmitted;
        public event EventHandler<MultipleEventArgs> OldTourSubmitted;

        public void OpenTour(int TourID, TourLog? tourLog)
        {
            _tourID = TourID;
            if (tourLog == null) { FillWithEmpty(); }
            else
            {
                _tourLogID = tourLog.ID;
                _tourComment = tourLog.comment;
                _date = tourLog.date.ToString();
                _difficulty = tourLog.difficulty.ToString();
                _totalTime = tourLog.totalTime.ToString();
                _rating = tourLog.rating.ToString();
            }
        }

        private void FillWithEmpty()
        {
            _tourLogID = null;
            TourComment = "";
            Date = "";
            Difficulty = "";
            TotalTime = "";
            Rating = "";
        }

        private TourLog ConvertToTour()
        {
            int id;
            if (_tourLogID == null)
            {
                id = 0;
            }
            else
            {
                id = (int)_tourLogID;
            }

            return new TourLog()
            {
                ID = id,
                comment = _tourComment,
                date = DateTime.Parse(_date),
                difficulty = Convert.ToInt32(_difficulty),
                totalTime = TimeSpan.Parse(_totalTime),
                rating = Convert.ToInt32(_rating),
            };
        }
    }
}
