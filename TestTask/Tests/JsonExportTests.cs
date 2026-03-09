using Core.Models;
using Core.Services;
using System.Text.Json;

namespace Tests
{
    public class JsonExportTests
    {
        [Fact]
        public async Task CreatingJsonFile()
        {
            var summaries = new List<WellSummary>
            {
                new WellSummary
                {
                    WellId = "A-001",
                    IntervalCount = 2,
                    TotalDepth = 20,
                    AvgPorosity = 0.15,
                    DominantRock = "Sandstone"
                }
            };

            var path = Path.GetTempFileName();

            var service = new JsonExport();

            await service.ExportData(summaries, path);

            Assert.True(File.Exists(path));

            var json = await File.ReadAllTextAsync(path);

            var result = JsonSerializer.Deserialize<List<WellSummary>>(json);

            Assert.Single(result);
            Assert.Equal("A-001", result[0].WellId);
        }
    }
}
