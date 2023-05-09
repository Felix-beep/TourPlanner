using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Linq;
using TourPlanner.BL.FullTextSearch;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class FullTextSearchTest
    {
        FullTextSearchFactory _factory;
        List<Tour> _tourList;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new();
            _tourList = new List<Tour>
            {
                new Tour(0, "Hello Subject1", "description1", new List<TourLog>()),
                new Tour(1, "Subject2", "description2", new List<TourLog>()),
                new Tour(2, "Subject3", "description3", new List<TourLog>()),
                new Tour(3, "Food", "Food", new List<TourLog>()),
            };

        }

        [TestCase("Subject", 0)]
        [TestCase("Subject1", 0)]
        [TestCase("Subject2", 1)]
        [TestCase("Subject3", 2)]
        [TestCase("kood", 3)]
        public void Test(string testString, int position) 
        {
            Console.WriteLine("Starting List:");
            foreach(var t in _tourList)
            {
                Console.WriteLine($"  Entry: {t.ID} and {t.name}");
            }

            List<Tour> SortedList = _factory.SearchForText(_tourList, testString).ToList();

            Console.WriteLine("Ending List:");
            foreach (var t in SortedList)
            {
                Console.WriteLine($"  Entry: {t.ID} and {t.name}");
            }
            Assert.That(SortedList[0].name, Is.EqualTo(_tourList[position].name));
        }
    }
}
