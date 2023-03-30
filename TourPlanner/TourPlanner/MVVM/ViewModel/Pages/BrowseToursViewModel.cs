using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class BrowseToursViewModel : ObservableObject
    {
        public List<Tour> Items { get; set; }

        public BrowseToursViewModel()
        {
            Items = new()
            {
                new Tour(1, "Hello", "some description"),
                new Tour(2, "Hallo", "some description"),
                new Tour(3, "Tschau", "some description")
            };
        }
    }
}
