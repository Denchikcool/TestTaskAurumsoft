namespace Core.Models
{
    public class Well
    {
        public string WellId { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }

        public List<Interval> Intervals { get; set; } = new();
    }
}
