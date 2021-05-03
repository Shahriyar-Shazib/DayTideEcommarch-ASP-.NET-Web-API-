using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class HomePage
    {
        public int totalEarning { get; set; }
        public int totalEmp { get; set; }
        public int task { get; set; }
        public int pendingRatio { get; set; }
        public int pendingRequest { get; set; }
        public int empAvailable { get; set; }
    }
}