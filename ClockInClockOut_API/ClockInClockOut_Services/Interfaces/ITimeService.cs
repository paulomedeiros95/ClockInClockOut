using ClockInClockOut_Domain.Domains;

namespace ClockInClockOut_Services.Interfaces
{
    public interface ITimeService
    {
        Task<TimeDomain> Add(TimeDomain time);

        Task<List<TimeDomain>> GetUserClocksPerMonth(int userId, DateTime month);

        Task<TimeDomain> Update(TimeDomain time);
    }
}