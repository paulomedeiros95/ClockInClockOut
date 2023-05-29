using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInClockOut_Dto.Response
{
    public class ReportResponseDto
    {
        public DateTime Mes { get; set; }
        public double HorasTrabalhadas { get; set; }
        public double HorasExcedentes { get; set; }
        public double HorasDevidas { get; set; }
        public List<ReportDetailResponseDto> Registros { get; set; }
    }
}
