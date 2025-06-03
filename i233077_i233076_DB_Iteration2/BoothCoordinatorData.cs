using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class BoothCoordinatorData
    {
        public static int SelectedCoordinatorId { get; set; }
        public static TimeSpan ShiftStartTime { get; set; }
        public static TimeSpan ShiftEndTime { get; set; }
        public static int boothId { get; set; }
    }
}
