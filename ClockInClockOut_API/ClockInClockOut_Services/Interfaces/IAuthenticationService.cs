using ClockInClockOut_Domain.Domains;

namespace ClockInClockOut_Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(string, UserDomain)> Authentication(UserDomain user);
    }
}
