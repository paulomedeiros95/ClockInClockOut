namespace ClockInClockOut_Dto.Response
{
    public class ProjectResponseDTO
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<int> Users { get; set; } = new List<int>();
    }
}
