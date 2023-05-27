﻿using AutoMapper;
using ClockInClockOut_API.MapperConfig;
using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Dto.Request;
using ClockInClockOut_Dto.Response;
using ClockInClockOut_Services.Exceptions;
using ClockInClockOut_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClockInClockOut_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AuthenticateController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion 

        #region Methods

        [HttpPost()]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 401)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 422)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<ActionResult> Login(
          [FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null)
                return UnprocessableEntity();

            try
            {
                var authUser = await _authenticationService.Authentication(_mapper.Map<UserDomain>(loginRequestDTO));
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO() { Token = authUser.Item1, User = _mapper.Map<UserResponseDTO>(authUser.Item2) };

                return Ok(loginResponseDTO);
            }
            catch (InvalidPasswordException ex)
            {
                return UnprocessableEntity(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return Unauthorized(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO() { Message = ex.Message });
            }
        }

        #endregion
    }
}
