using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using Microsoft.VisualBasic; 

namespace TourPlanner.BL.FullTextSearch
{
    internal class NameSearchElement : BaseSearchElementDecorator
    {
        public NameSearchElement(ITextSearchElement nextElement) : base(nextElement) 
        {
            // performs the exact same behavior as its parent
        }

        public override double CompareFunction(Tour tourToRate, string text)
        {
            string str1 = tourToRate.name;
            string str2 = text;


            Console.WriteLine($"\nComparing \"{str1}\" to \"{str2}\"");

            int distance = StringComparer.CompareStrings(str1, str2);
            double similarity = 1.0 - (double)distance / Math.Max(str1.Length, str2.Length);
            return similarity;
        }
    }
}
