using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.FullTextSearch.Decorators;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch
{
    public class FullTextSearchFactory
    {
        private ITextSearchElement _startingDecorator;

        private List<ITextSearchElement> specificTourDecorators = new List<ITextSearchElement>() {
            new NameSearchDecorator(),
            new DescriptionSearchDecorator(),
            new StartingLocationSearchDecorator(),
            new EndingLocationSearchDecorator(),
            new TransportSearchDecorator(),
        };

        private List<ITextSearchElement> specificTourLogDecorators = new List<ITextSearchElement>()
        {
            new TourLogCommentSearchDecorator(),
        };

        private List<ITextSearchElement> FullTextDecorators = new List<ITextSearchElement>()
        {
            new FullTourSearchDecorator(),
            new FullTourLogSearchDecorator(),
        };

        public FullTextSearchFactory() 
        {
            _startingDecorator = null;

            List<ITextSearchElement> DecoratorsToUse = FullTextDecorators;

            foreach(ITextSearchElement Decorator in DecoratorsToUse)
            {
                AddDecorator(Decorator);
            }
        }

        private void AddDecorator(ITextSearchElement Decorator)
        {
            if (Decorator == null){
                _startingDecorator = null; 
                return; 
            }

            Decorator.AddDecorator(_startingDecorator);
            _startingDecorator = Decorator;
        }


        public IEnumerable<Tour> SearchForText(List<Tour> listOfAllTours, string text)
        {
            Dictionary<double, List<Tour>> Dictionary = new();

            List<double> Ratings = new();
            
            foreach(Tour t in listOfAllTours) 
            {
                double Rating = Rate(t, text);
                if(!Dictionary.ContainsKey(Rating))
                {
                    Dictionary.Add(Rating, new List<Tour> { t });
                } else
                {
                    Dictionary[Rating].Add(t);
                }
                Ratings.Add(Rating);
            }

            Ratings.Sort();

            List<Tour> SortedList = new();

            foreach (double r in Ratings)
            {
                SortedList.Add(Dictionary[r][0]);
                Dictionary[r].RemoveAt(0);
            }

            return SortedList;
        }

        private double Rate(Tour tour, String text)
        {            
            return _startingDecorator.Rate(tour, text);
        }
    }
}
