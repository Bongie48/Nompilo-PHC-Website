using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Nompilo_PHC_Website.Data;

namespace Nompilo_PHC_Website.Models
{
    public class EverithingPeriodsF
    {
        [Key]
        public int PeriodId { get; set; }
        [Required(ErrorMessage = "Please select the day your period started")]
        [DisplayName("Period Start Day")]
        public DateTime Start { get; set; }
        public string Symptoms { get; set; }
        public string Year { get; set; }
        public string MonthYear { get; set; }
        public int DayDifference { get; set; }
        public string Abnormality { get; set; }
        [DisplayName("Period End Day")]
        public DateTime End { get; set; }
        public DateTime Ovulation1 { get; set; }
        [DisplayName("Your fertility be from day: ")]
        public DateTime PregnancyPrediction { get; set; }
        public DateTime PregLastDay { get; set; }
        [DisplayName("Next Period is in: ")]
        public DateTime NextPeriod { get; set; }
        public string EmailAdd { get; set; }
    }
}
