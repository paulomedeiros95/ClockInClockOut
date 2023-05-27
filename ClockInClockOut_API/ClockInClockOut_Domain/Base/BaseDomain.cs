using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInClockOut_Domain.Base
{
    public abstract class BaseDomain
    {
        #region Properties

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedAt { get; set; }

        #endregion
    }
}
