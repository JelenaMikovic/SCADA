using System.ComponentModel.DataAnnotations;

namespace scada_back.Models
{
    public class Alarm
    {
            [Key]
            public int Id { get; set; }
            public int TagId { get; set; }

            public double Value { get; set; }

            public Type Type { get; set; }

            public Priority Priority { get; set; }

    }

    public enum Priority
    {
        LOW,
        MEDIUM,
        HIGH
    }

    public enum Type
    {
        LOWER,
        HIGHER
    }
}