﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class CreateTourLogsViewModel : ObservableObject
    {
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
            }
        }

        public CreateTourLogsViewModel()
        {
            FillWithEmpty();
            SubmitForm = new RelayCommand(param => {
                if (_tourLogID == null)
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

        public event EventHandler<TourLog> NewTourSubmitted;
        public event EventHandler<TourLog> OldTourSubmitted;

        public void OpenTour(TourLog? tourLog)
        {
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
            _tourComment = "";
            _date = "";
            _difficulty = "";
            _totalTime = "";
            _rating = "";
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
                //date = (DateTime)_date,
                difficulty = Convert.ToInt32(_difficulty),
                //totalTime = _totalTime,
                rating = Convert.ToInt32(_rating),
            };
        }
    }
}
