using System;

namespace Core.Entities
{
    public class Keyword
    {
        public Guid Id { get; set; }
        public Guid RecordId { get; set; }
        public Record Record { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public double Significance { get; set; }
        public bool Positive { get; set; }
    }
}
