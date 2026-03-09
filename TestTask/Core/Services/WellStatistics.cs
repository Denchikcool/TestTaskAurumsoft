using Core.Models;

namespace Core.Services
{
    public class WellStatistics
    {
        public WellSummary Calculate(Well well)
        {
            double totalDepth = well.Intervals.Max(i => i.DepthTo);

            double totalLength = well.Intervals.Sum(i => i.Length);

            double avgPorosity = well.Intervals.Sum(i => i.Porosity * i.Length) / totalLength;

            string dominantRock = 
                well.Intervals
                .GroupBy(i => i.Rock)
                .Select(g => new 
                { 
                    Rock = g.Key, 
                    Thickness = g.Sum(i => i.Length) 
                })
                .OrderByDescending(x => x.Thickness)
                .First()
                .Rock;

            return new WellSummary
            {
                WellId = well.WellId,
                IntervalCount = well.Intervals.Count,
                TotalDepth = totalDepth,
                AvgPorosity = avgPorosity,
                DominantRock = dominantRock
            };
        }
    }
}
