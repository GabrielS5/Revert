using System;

namespace Core.Entities.Queries
{
    public class RecordsQuery
    {
        public Guid? PatientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public override string ToString()
        {
            var str = "";

            str = str + "\n PatientId: " + PatientId.ToString();
            str = str + "\n StartDate: " + StartDate.ToString();
            str = str + "\n EndDate: " + EndDate.ToString();

            return str;
        }
    }
}
