namespace TaskManager.Api.DTOs
{
    public class TaskAdditionalDataDto
    {
        public string? Priority { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public List<string>? Tags { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}
