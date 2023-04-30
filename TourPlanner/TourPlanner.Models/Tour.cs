using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Models
{
    public class Tour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string from { get; set; }
        public string to { get; set; }
        public string transportType { get; set; }
        public double tourDistance { get; set; }
        public DateTime estimatedTime { get; set; }
        public string imageID { get; set; }

        public ICollection<TourLog> logs { get; set; }

        public Tour() { }

        public Tour(int ID, string name, string description, ICollection<TourLog> logs)
        {
            this.ID = ID;
            this.name = name;
            this.description = description;
            this.logs = logs;
        }

        public override string ToString() => $"Tour({ID},{name},{description},{logs.Count})";
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is Tour otherTour) 
            {
                return otherTour.ID == ID; 
            }

            return false;
        }
    }
}
