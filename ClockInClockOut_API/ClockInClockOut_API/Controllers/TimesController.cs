using AutoMapper;
using ClockInClockOut_API.Controllers.Base;
using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Dto.Request;
using ClockInClockOut_Dto.Response;
using ClockInClockOut_Services.Exceptions;
using ClockInClockOut_Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ClockInClockOut_API.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TimesController : BaseController
    {
        #region Fields

        private readonly ITimeService _timeService;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public TimesController(ITimeService timeService, IMapper mapper)
        {
            _timeService = timeService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        [HttpGet("folhas-de-ponto/{mes}")]
        [ProducesResponseType(typeof(List<TimeResponseDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 422)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> Get(string mes)
        {
            var userId = (int)GetUserId();

            if (userId == 0)
            {
                return UnprocessableEntity(new ErrorResponseDTO { Message = "Invalid User" });
            }

            try
            {
                DateTime monthConverted;
                if (DateTime.TryParseExact(mes, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out monthConverted)) ;
                
                var times = await _timeService.GetUserClocksPerMonth(userId, monthConverted);

                return Ok(_mapper.Map<List<TimeResponseDTO>>(times));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponseDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO { Message = ex.Message });
            }
        }

        [HttpPost("bater")]
        [ProducesResponseType(typeof(TimeResponseDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 409)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 422)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> Bater([FromBody] TimeRequestDTO timeRequestDTO)
        {
            if (timeRequestDTO == null)
            {
                return UnprocessableEntity(new ErrorResponseDTO { Message = "Invalid time posting data" });
            }

            var userId = (int)GetUserId();

            if (userId == 0)
            {
                return UnprocessableEntity(new ErrorResponseDTO { Message = "Invalid User" });
            }

            try
            {

                var timeDomain = _mapper.Map<TimeDomain>(timeRequestDTO);

                timeDomain.UserID = userId;

                var time = await _timeService.Add(timeDomain);

                return Ok(_mapper.Map<TimeResponseDTO>(time));
            }
            catch (DuplicateItemException ex)
            {
                return Conflict(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (RequiredFieldException ex)
            {
                return UnprocessableEntity(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO() { Message = ex.Message });
            }
        }

        [HttpPut("{timeID}")]
        [ProducesResponseType(typeof(TimeResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 422)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> Update(int timeID, [FromBody] TimeRequestDTO timeRequestDTO)
        {
            if (timeRequestDTO == null)
            {
                return UnprocessableEntity(new ErrorResponseDTO { Message = "Invalid time posting data" });
            }

            try
            {
                var time = _mapper.Map<TimeDomain>(timeRequestDTO);
                time.ID = timeID;

                var updatedTime = await _timeService.Update(time);
                return Ok(_mapper.Map<TimeResponseDTO>(updatedTime));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (DuplicateItemException ex)
            {
                return Conflict(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (RequiredFieldException ex)
            {
                return UnprocessableEntity(new ErrorResponseDTO() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO() { Message = ex.Message });
            }
        }

        #endregion
    }
}
