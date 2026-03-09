using Core.Models;
using Core.Services;

namespace Tests
{
    public class ValidatorTests
    {
        private readonly WellValidator _validator = new();

        [Fact]
        public void Error_DepthFromGreaterDepthTo()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 10, DepthTo = 5, Porosity = 0.2, Rock = "Sandstone"}
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Contains(result, e => e.ErrorMessage.Contains("DepthFrom должно быть строго меньше DepthTo"));
        }

        [Fact]
        public void Error_DepthFromNegative()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = -5, DepthTo = 5, Porosity = 0.2, Rock = "Sandstone"}
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Contains(result, e => e.ErrorMessage.Contains("DepthFrom должен быть неотрицательным"));
        }

        [Fact]
        public void Error_WrongPorosity()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 5, Porosity = 2.2, Rock = "Sandstone"}
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Contains(result, e => e.ErrorMessage.Contains("Porosity должно находиться в диапазоне [0..1]"));
        }

        [Fact]
        public void Error_EmptyRock()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 5, Porosity = 0.2, Rock = ""}
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Contains(result, e => e.ErrorMessage.Contains("Rock не должен быть пустым"));
        }

        [Fact]
        public void Error_OverlapIntervals()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 10, Porosity = 0.2, Rock = "Limestone"},
                    new Interval {DepthFrom = 8, DepthTo = 20, Porosity = 0.3, Rock = "Shale"},
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Contains(result, e => e.ErrorMessage.Contains("Интервалы по одной скважине не должны пересекаться"));
        }

        [Fact]
        public void ValidData()
        {
            var well = new Well
            {
                WellId = "A1",
                Intervals =
                [
                    new Interval {DepthFrom = 0, DepthTo = 10, Porosity = 0.2, Rock = "Limestone"},
                    new Interval {DepthFrom = 10, DepthTo = 20, Porosity = 0.3, Rock = "Shale"},
                ]
            };

            var result = _validator.Validate([well]);

            Assert.Empty(result);
        }
    }
}
