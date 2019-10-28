using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Record
    {
        public Guid Id { get; set; }
        public string Patient { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; }
    }
}
