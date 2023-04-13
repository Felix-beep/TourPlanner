using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Models
{
    public class TourLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string comment { get; set; }

        public TourLog() { }

        public TourLog(int ID, string comment) 
        {
            this.ID = ID;
            this.comment = comment;
        }

        public override string ToString() => $"TourLog({ID},{comment})";
    }
}
