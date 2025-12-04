namespace KundenUmfrageTool.Api.Dtos.Reports
{
    public class RestaurantReportDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;

        public List<CheckpointAverageDto> Averages { get; set; } = new();
        public List<CommentDto> Comments { get; set; } = new();

        public CommentDto? BestComment { get; set; }
        public CommentDto? WorstComment { get; set; }
    }
}
