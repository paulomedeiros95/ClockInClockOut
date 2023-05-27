using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClockInClockOut_Domain.Domains;

namespace ClockInClockOut_Infra.Builders
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDomain>
    {
        public void Configure(EntityTypeBuilder<UserDomain> builder)
        {
            builder.HasKey(k => k.ID);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
