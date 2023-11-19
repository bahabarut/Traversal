using System.Collections.Generic;
using System;

namespace SignalRApiForSql.Models
{
    public class VisitorChart
    {
        public VisitorChart()
        {
            Counts = new List<int>();
        }
        public DateTime VisitDate { get; set; }
        public List<int> Counts { get; set; }
    }
}
