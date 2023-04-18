using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Models
{
    public class Tour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public ICollection<TourLog> logs { get; set; }

        public Tour() { }

        public Tour(int ID, string name, string description, ICollection<TourLog> logs)
        {
            this.ID = ID;
            this.name = name;
            this.description = description;
            this.logs = logs;
        }

        public override string ToString() => $"Tour({ID},{name},{description},{logs?.Count})";
    }
}
