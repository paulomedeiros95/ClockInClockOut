using ClockInClockOut_Domain.Domains;

namespace ClockInClockOut_Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDomain> Add(UserDomain user);

        Task<UserDomain> Find(int id);

        Task<UserDomain> Update(UserDomain user);
    }
}