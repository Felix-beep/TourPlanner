using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch.Decorators
{
    internal class StartingLocationSearchDecorator : BaseSearchElementDecorator
    {
        public override double CompareFunction(Tour tourToRate, string text)
        {
            string str1 = tourToRate.from;
            string str2 = text;


            Console.WriteLine($"\nComparing \"{str1}\" to \"{str2}\"");

            int distance = StringComparer.CompareStrings(str1, str2);

            double similarity = distance;
            return similarity;
        }
    }
}
