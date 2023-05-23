using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch
{
    internal class BaseSearchElementDecorator : ITextSearchElement
    {

        public ITextSearchElement _nextElement;

        public BaseSearchElementDecorator(ITextSearchElement nextElement)
        {
            _nextElement = nextElement;
        }

        public double Rate(Tour tourToRate, string text)
        {
            double thisValue = CompareFunction(tourToRate, text);

            Console.WriteLine($"Gave a total rating of {thisValue}");

            if (_nextElement == null) return thisValue;

            double nextValue = _nextElement.Rate(tourToRate, text);

            return (thisValue < nextValue) ? thisValue : nextValue;
        }

        public virtual double CompareFunction(Tour tourToRate, string text)
        {
            return 0;
        }
    }
}
