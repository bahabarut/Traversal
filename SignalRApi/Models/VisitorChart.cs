using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SignalRApi.Models
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
