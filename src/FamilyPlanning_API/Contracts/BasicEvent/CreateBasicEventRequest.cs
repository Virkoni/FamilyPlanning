namespace FamilyPlanning_API.Contracts.BasicEvent
{
    public class CreateBasicEventRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
