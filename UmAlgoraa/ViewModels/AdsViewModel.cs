using System.ComponentModel.DataAnnotations.Schema;

namespace UmAlgoraa.ViewModels
{
    [NotMapped]
    public class AdsViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Competition { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime Deadline { get; set; }
        public double PampletValue { get; set; }
        public int CompetitionNum { get; set; }
        public string Detalies { get; set; }
        public string Notes { get; set; }
        public bool IsDrafted { get; set; }

        // Don't Use this Except in New or Edit View
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
    }
}
