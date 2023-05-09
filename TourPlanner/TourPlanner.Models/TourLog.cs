using System.ComponentModel.DataAnnotations.Schema;

namespace TourPlanner.Models
{
    public class TourLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string comment { get; set; }

        public DateTime date { get; set; }
        public int difficulty { get; set; }
        public DateTime totalTime { get; set; }
        public int rating { get; set; }

        public TourLog() { }

        public TourLog(int ID, string comment) 
        {
            this.ID = ID;
            this.comment = comment;
        }

        public override string ToString() => $"TourLog({ID},{comment})";

        public void CopyFrom(TourLog other)
        {
            ID = other.ID;
            comment = other.comment;
            date = other.date;
            difficulty = other.difficulty;
            totalTime = other.totalTime;
            rating = other.rating;
        }
    }
}
