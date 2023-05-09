using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch
{
    public class FullTextSearchFactory
    {
        private BaseSearchElementDecorator _startingDecorator;


        public FullTextSearchFactory() {
            _startingDecorator = new NameSearchElement(new DescriptionSearchElement(null));
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
            if (tour == null)
            {
                Console.WriteLine("Tour not found.");
                return 0;
            }
            
            return _startingDecorator.Rate(tour, text);
        }
    }
}
