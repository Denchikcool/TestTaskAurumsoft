namespace Core.Models
{
    public class Interval
    {
        public double DepthFrom {  get; set; }
        public double DepthTo { get; set; }
        public string Rock { get; set; } = "";
        public double Porosity { get; set; }
        public int RowNumber { get; set; }

        public double Length => DepthTo - DepthFrom; 
    }
}
