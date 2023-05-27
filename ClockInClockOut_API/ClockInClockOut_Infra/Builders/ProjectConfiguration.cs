using ClockInClockOut_Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClockInClockOut_Infra.Builders
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectDomain>
    {
        public void Configure(EntityTypeBuilder<ProjectDomain> builder)
        {
            builder.HasKey(k => k.ID);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).IsRequired();
        }
    }
}
