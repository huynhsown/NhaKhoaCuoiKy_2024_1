using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    public class EmptyScheduleModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }   
        public EmptyScheduleModel(DateTime Start, DateTime End)
        {
            this.Start = Start;
            this.End = End;
        }

        public override string ToString()
        {
            return Start.TimeOfDay.ToString() + " - " + End.TimeOfDay.ToString();
        }
    }
}
