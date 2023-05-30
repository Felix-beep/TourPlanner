using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using Microsoft.VisualBasic;

namespace TourPlanner.BL.FullTextSearch.Decorators
{
    internal class NameSearchDecorator : BaseSearchElementDecorator
    {
        public override double CompareFunction(Tour tourToRate, string searchtext)
        {
            string tourstring = tourToRate.name;

            Console.WriteLine($"\nComparing \"{tourstring}\" to \"{searchtext}\"");

            int distance = StringComparer.CompareStrings(tourstring, searchtext);
            double similarity = distance;
            return similarity;
        }
    }
}
