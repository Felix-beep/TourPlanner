using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;

namespace TourPlanner.BL
{
    public class TourPlanner : ITourPlanner
    {
        ITourRepository tourRepo;

        public TourPlanner(ITourRepository tourRepo) 
        {
            this.tourRepo = tourRepo;
        }
    }
}
