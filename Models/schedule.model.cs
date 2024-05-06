using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    public class ScheduleModel
    {
        public int ScheduleID { get; set; }
        public string ScheduleType { get; set; }
        public int ScheduleTypeID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return ScheduleID + " " + ScheduleType + " " + ScheduleTypeID + " " + Start.ToString() + " " + End.ToString();
        }
    }
}
