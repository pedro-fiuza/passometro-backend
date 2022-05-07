namespace Domain.Dto
{
    public class CreateEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.UtcNow;
        public int ResumeId { get; set; }
    }
}
