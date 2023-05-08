using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch
{
    internal interface ITextSearchElement
    {
        public double Rate(Tour tourToRate, string text);
    }
}
