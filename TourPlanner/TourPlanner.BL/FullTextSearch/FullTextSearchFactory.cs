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
            List<Tour> SortedList = new List<Tour>(listOfAllTours);

            SortedList.Sort((s1, s2) => Rate(s2, text).CompareTo(Rate(s1, text)));

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
