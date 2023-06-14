using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace UmAlgoraa.Models
{
    public enum Status
    {
        Publish=1,
        UnderRevision=2,
        Refused=3,
        Drafted=4
    }

    public class Ads
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Competition { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime Deadline { get; set; }
        public double PampletValue { get; set; }
        public int CompetitionNum { get; set; }
        public string Detalies { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }

        [DefaultValue("تحت المراجعة")]
        public string AdStatus { get; set; }

        public bool IsDrafted { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        

    }
}
