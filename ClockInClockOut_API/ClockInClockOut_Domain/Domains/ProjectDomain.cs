using ClockInClockOut_Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInClockOut_Domain.Domains
{
    public class ProjectDomain: BaseDomain
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<UserDomain> Users { get; set; } = new List<UserDomain>();

        public ICollection<TimeDomain> Times { get; set; } = new HashSet<TimeDomain>();
    }
}
