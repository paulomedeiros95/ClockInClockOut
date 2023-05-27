using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClockInClockOut_Domain.Domains;

namespace ClockInClockOut_Infra.Builders
{
    public class TimeConfiguration : IEntityTypeConfiguration<TimeDomain>
    {
        public void Configure(EntityTypeBuilder<TimeDomain> builder)
        {
            builder.HasKey(k => k.ID);
            builder.Property(p => p.EndedAt).IsRequired();
            builder.Property(p => p.StartedAt).IsRequired();
        }
    }
}
