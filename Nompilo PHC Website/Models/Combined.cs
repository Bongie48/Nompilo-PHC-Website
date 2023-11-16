using iText.Layout.Element;

namespace Nompilo_PHC_Website.Models
{
    public class Combined
    {
        public List<Report> Report { get; set; }
        public IEnumerable<PrenatalPatients> Patients { get; set; }
    }
}
