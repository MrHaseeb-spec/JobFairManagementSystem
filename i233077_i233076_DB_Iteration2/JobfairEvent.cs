using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class JobFairEvent
    {
        public int JobFairEventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Location { get; set; }
        public int AdminId { get; set; }
    }

}
