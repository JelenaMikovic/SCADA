namespace RTU
{
    public class Device
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public DeviceType Type { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }

    }
    public enum DeviceType
    {
        AI,
        DI
    }

}
