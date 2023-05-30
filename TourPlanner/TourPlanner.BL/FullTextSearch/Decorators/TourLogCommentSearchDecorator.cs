using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.FullTextSearch.Decorators
{
    internal class TourLogCommentSearchDecorator : BaseSearchElementDecorator
    {
        public override double CompareFunction(Tour tourToRate, string text)
        {
            double lowestScore = double.MaxValue;

            foreach(TourLog Log in tourToRate.logs) {
                string str1 = Log.comment;
                string str2 = text;

                Console.WriteLine($"\nComparing \"{str1}\" to \"{str2}\"");

                double rating = StringComparer.CompareStrings(str1, str2);

                if(rating < lowestScore)
                {
                    lowestScore = rating;
                }
            }

            double similarity = lowestScore;
            return similarity;
        }
    }
}
