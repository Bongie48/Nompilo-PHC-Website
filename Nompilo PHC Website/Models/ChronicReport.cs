namespace Nompilo_PHC_Website.Models
{
    public class ChronicReport
    {
        public QueryReport queryReport { get; set; }
        public List<QueryReport> reports { get; set; }
        public IEnumerable<Reminder> Rem { get; set; }
        public IEnumerable<Chronicpatients> Chrcpatients { get; set; }
        public IEnumerable<Rvalues> Scores { get; set; }
    }
}
