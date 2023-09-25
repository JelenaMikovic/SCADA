using System.ComponentModel.DataAnnotations;

namespace scada_back.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public DeviceType Type { get; set; }
    }

    public enum DeviceType
    {
        AI,
        DI
    }
}
