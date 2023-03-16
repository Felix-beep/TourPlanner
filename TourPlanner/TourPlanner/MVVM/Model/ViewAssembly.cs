using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.MVVM.View;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner.MVVM.Model
{
    public static class ViewAssembly
    {
        public static ViewObject HomeViewObject = new ViewObject
        {
            Title = "Homepage",
            Content = new HomeViewModel(),
            Utils = new SearchbarViewModel((_, searchText) => ViewAssembly.HomeViewObject.Title = searchText),
        };
    }
}
