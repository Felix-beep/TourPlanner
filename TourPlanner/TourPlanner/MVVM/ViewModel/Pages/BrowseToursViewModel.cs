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
                new Tour{
                    ID=1,
                    name="Hello",
                    description="some description"},
                new Tour{
                    ID=2,
                    name="Hallo",
                    description="some description"},
                new Tour{
                    ID=3,
                    name="Tschau",
                    description="some description"},
            };
        }
    }
}
