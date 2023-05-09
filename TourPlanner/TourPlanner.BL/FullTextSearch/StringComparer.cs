using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.FullTextSearch
{
    internal static class StringComparer
    {
        public static Int32 CompareStrings(string a, string b)
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            string longer;
            string shorter;
            if(a.Length < b.Length)
            {
                shorter = a;
                longer = b;
            } else
            {
                longer = a;
                shorter = b;
            }

            int smallestDifference = 0;

            int numOfPossibleSubstrings = longer.Length - shorter.Length + 1;

            for(int i = 0; i < numOfPossibleSubstrings; i++)
            {

                Console.WriteLine($"Creating Substring {i+1}/{numOfPossibleSubstrings} from {longer} with length of {shorter.Length}:");

                string substring = longer.Substring(i, shorter.Length);

                Console.WriteLine($"Substring created: {substring}");

                int substringRating = LevenshteinDistance(substring, shorter);

                Console.WriteLine($"Comparing \"{substring}\" to \"{shorter}\"\nResulting distance: {substringRating}\n");

                if (i == 0)
                {
                    smallestDifference = substringRating;
                } else if(substringRating < smallestDifference)
                {
                    smallestDifference = substringRating;
                }
            }

            return smallestDifference;
        }

        private static Int32 LevenshteinDistance(String a, String b)
        {
            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1; 
                    min2 = d[i, j - 1] + 1; 
                    min3 = d[i - 1, j - 1] + cost; 
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }
    }
}
