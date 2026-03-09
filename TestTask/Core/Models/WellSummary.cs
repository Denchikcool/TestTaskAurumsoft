namespace Core.Models
{
    public class WellSummary
    {
        public string WellId { get; set; } = "";
        public int IntervalCount { get; set; }
        public double TotalDepth { get; set; }
        public double AvgPorosity { get; set; }
        public string DominantRock { get; set; } = "";
    }
}
