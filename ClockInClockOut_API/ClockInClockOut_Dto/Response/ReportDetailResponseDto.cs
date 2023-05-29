using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInClockOut_Dto.Response
{
    public class ReportDetailResponseDto
    {
        public DateTime Day { get; set; }
        public List<TimeOnly> Horarios { get; set; }
    }
}
