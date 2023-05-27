using ClockInClockOut_Domain.Base;
using ClockInClockOut_Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ClockInClockOut_Infra.Seed
{

    [ExcludeFromCodeCoverage]
    public static class ModelBuilderExtension
    {

        private static void SeedUserTest(ModelBuilder modelBuilder)
        {
            List<UserDomain> user = new List<UserDomain>()
            {
                new UserDomain() { ID = 1, Name= "User Default", Email="UserTest@TestApi.com", CreatedDate = DateTime.Now, Login = "test", Password="test123" },
            };

            modelBuilder.SetDataToEntity<UserDomain>(user);
        }

        private static void SetDataToEntity<T>(this ModelBuilder modelBuilder, List<T> data) where T : BaseDomain
        {
            modelBuilder.Entity<T>().HasData(data);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedUserTest(modelBuilder);
        }
    }
}
