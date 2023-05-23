﻿using System;
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

        public override double CompareFunction(Tour tourToRate, string searchtext)
        {
            string tourstring = tourToRate.name;

            Console.WriteLine($"\nComparing \"{tourstring}\" to \"{searchtext}\"");

            int distance = StringComparer.CompareStrings(tourstring, searchtext);
            double similarity = (double)distance;
            return similarity;
        }
    }
}
