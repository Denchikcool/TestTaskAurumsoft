using Core.Models;
using Core.Services;

namespace Tests
{
    public class StatisticsTest
    {
        [Fact]
        public void CalculateTotalDepth()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 10.10, Porosity = 0.1, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 50, Porosity = 0.3, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 53.78, Porosity = 0.3, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 42.88, Porosity = 0.3, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 85.97, Porosity = 0.3, Rock = "Sand"}
                ]
            };

            var service = new WellStatistics();

            var result = service.Calculate(well);

            Assert.Equal(85.97, result.TotalDepth, 2);
        }

        [Fact]
        public void CalculateAvgPorosity()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals = 
                [
                    new Interval {DepthFrom = 0, DepthTo = 10, Porosity = 0.1, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Sand"}
                ]
            };

            var service = new WellStatistics();

            var result = service.Calculate(well);

            Assert.Equal(0.2, result.AvgPorosity, 2);
        }

        [Fact]
        public void CalculateDominantRock()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 10, Porosity = 0.1, Rock = "Limestone"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Sand"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Sandstone"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Limestone"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Shale"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Sandstone"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Sandstone"}
                ]
            };

            var service = new WellStatistics();

            var result = service.Calculate(well);

            Assert.Equal("Sandstone", result.DominantRock);
        }
    }
}