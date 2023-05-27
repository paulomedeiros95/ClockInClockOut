namespace ClockInClockOut_Dto.Response
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public UserResponseDTO User { get; set; } = new UserResponseDTO();
    }
}
