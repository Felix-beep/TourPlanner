using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Models
{
    public class Tour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Tour() { }

        public Tour(int ID, string name, string description)
        {
            this.ID = ID;
            this.name = name;
            this.description = description;
        }

        public override string ToString() => $"Tour({ID},{name},{description})";
    }
}
