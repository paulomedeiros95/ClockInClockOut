namespace ClockInClockOut_Dto.Request
{
    public class ProjectRequestDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<int> Users { get; set; } = new List<int>();
    }
}
