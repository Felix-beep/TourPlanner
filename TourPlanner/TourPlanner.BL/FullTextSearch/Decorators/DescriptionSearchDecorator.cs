﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using Microsoft.VisualBasic;

namespace TourPlanner.BL.FullTextSearch.Decorators
{
    internal class DescriptionSearchDecorator : BaseSearchElementDecorator
    {
        public override double CompareFunction(Tour tourToRate, string text)
        {
            string str1 = tourToRate.description;
            string str2 = text;


            Console.WriteLine($"\nComparing \"{str1}\" to \"{str2}\"");

            int distance = StringComparer.CompareStrings(str1, str2);

            double similarity = distance;
            return similarity;
        }
    }
}
