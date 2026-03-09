using Core.Models;
using Core.Services;

namespace Tests
{
    public class CsvLoaderTests
    {
        [Fact]
        public void ValidReadFromCSV()
        {
            var csv =
                """
                A-001;82.10;55.20;0;10;Sandstone;0.18
                A-001;82.10;55.20;10;25;Limestone;0.07
                """;

            var path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            var loader = new CsvLoader();

            var (wells, errors) = loader.Load(path);

            Console.WriteLine($"Wells: {wells.Count}");
            Console.WriteLine($"Errors: {errors.Count}");

            Assert.Single(wells);
            Assert.Empty(errors);

            var well = wells.First();

            Assert.Equal("A-001", well.WellId);
            Assert.Equal(2, well.Intervals.Count);
        }

        [Fact]
        public void Error_InvalidData()
        {
            var csv =
                """
                A-001;82.10;55.20;HELLO;10;Sandstone;0.18
                """;

            var path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            var loader = new CsvLoader();

            var (wells, errors) = loader.Load(path);

            Assert.Empty(wells);
            Assert.NotEmpty(errors);
        }
    }
}
