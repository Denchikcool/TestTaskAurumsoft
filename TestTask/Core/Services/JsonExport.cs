using System.Text.Json;
using Core.Models;

namespace Core.Services
{
    public class JsonExport
    {
        public async Task ExportData(List<WellSummary> summaries, string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(summaries, options);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
