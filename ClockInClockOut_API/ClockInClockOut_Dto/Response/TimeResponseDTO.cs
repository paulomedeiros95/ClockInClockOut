namespace ClockInClockOut_Dto.Response
{
    public class TimeResponseDTO
    {
        public int ID { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.MinValue;

        public DateTime EndedAt { get; set; } = DateTime.MinValue;

        public int UserID { get; set; }

        public int ProjectID { get; set; }
    }
}
