using System.Security.Claims;

namespace scada_back.Models
{
    public class AITag : Tag
    {
        public int ScanTime { get; set; }
        public bool IsScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

    }


}
