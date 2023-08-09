namespace scada_back.Models
{
    public class AOTag : Tag
    {
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }
}
