using AutoMapper;
using ClockInClockOut_CrossCutting;
using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Infra.Interfaces;
using ClockInClockOut_Services.Exceptions;
using ClockInClockOut_Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ClockInClockOut_Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        private readonly IBaseRepository<UserDomain> _userRepository;

        #endregion

        #region Constructor

        public AuthenticationService(
            IMapper mapper,
            IBaseRepository<UserDomain> userRepository,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        #endregion

        #region Methods
        public async Task<(string, UserDomain)> Authentication(UserDomain user)
        {
            if ((String.IsNullOrEmpty(user.Login) || String.IsNullOrEmpty(user.Password)))
                throw new InvalidPasswordException("Email or password not filled in");

            UserDomain userDB = await _userRepository.SelectFirstBy(x => x.Login == user.Login && x.Password == user.Password);

            if (userDB == null)
                throw new NotFoundException("Unable to login, user not found");

            var jwtSecret = _configuration.GetSection("JwtSecret").Value ?? string.Empty;

            string token = Token.CreateToken(new Dictionary<string, string>()
            {
                {"email", user.Email},
                {"id", userDB.ID.ToString() },
            }, jwtSecret);

            return (token, userDB);
        }

        #endregion
    }
}
