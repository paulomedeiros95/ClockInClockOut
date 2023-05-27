using ClockInClockOut_Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClockInClockOut_Domain.Domains
{
    public class TimeDomain : BaseDomain
    {
        public int ID { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.MinValue;

        public DateTime EndedAt { get; set; } = DateTime.MinValue;

        [ForeignKey("User")]
        public int UserID { get; set; }

        public UserDomain User { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        public ProjectDomain Project { get; set; }
    }
}
