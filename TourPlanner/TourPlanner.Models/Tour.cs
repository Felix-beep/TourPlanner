using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public TimeSpan estimatedTime { get; set; }
        public string imageID { get; set; }

        public float rating { get { return logs.Count(); } set { return; } }

        public float childfriendliness { get { return (float)GetChildFriendliness(); } set { return; } }

        public ICollection<TourLog> logs { get; set; }

        public Tour() { }

        public Tour(int ID, string name, string description, ICollection<TourLog> logs)
        {
            this.ID = ID;
            this.name = name;
            this.description = description;
            this.logs = logs;
        }

        public string CustomToString()
        {
            var sb = new StringBuilder("Tour(");
            sb.Append(ID);
            sb.Append(',');
            sb.Append(name ?? "-");
            sb.Append(',');
            sb.Append(description ?? "-");
            sb.Append(',');
            sb.Append(from ?? "-");
            sb.Append(',');
            sb.Append(to ?? "-");
            sb.Append(',');
            sb.Append(transportType ?? "-");
            sb.Append(',');
            sb.Append(tourDistance);
            sb.Append(',');
            sb.Append(estimatedTime);
            sb.Append(',');
            sb.Append(imageID ?? "-");
            sb.Append(',');
            sb.Append(logs?.Count);
            sb.Append(')');
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is Tour otherTour) 
            {
                return otherTour.ID == ID; 
            }

            return false;
        }

        public double GetChildFriendliness()
        {
            // lower value -> more child friendly

            return
                GetAverageDifficulty() *
                tourDistance *
                GetAverageTime();
        }

        public double GetAverageTime()
        {
            if (logs.Count == 0) return 0;
            return logs.Average(tl => tl.totalTime.TotalSeconds);
        }

        public double GetAverageDifficulty()
        {
            if (logs.Count == 0) return 0;
            return logs.Average(tl => tl.difficulty);
        }
    }
}
