using System.Globalization;
using Core.Models;

namespace Core.Services
{
    public class CsvLoader
    {
        public (List<Well>, List<ValidationError>) Load(string path)
        {
            Dictionary<string, Well> wells = new Dictionary<string, Well>();
            List<ValidationError> errors = new List<ValidationError>();

            IEnumerable<string> lines = File.ReadAllLines(path);

            int row = 0;

            foreach(string line in lines)
            {
                row++;

                string[] parts = line.Split(';');

                if(parts.Length != 7)
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = row,
                        WellId = parts.Length > 0 ? parts[0] : "",
                        ErrorMessage = "Неверное количество колонок"
                    });
                    continue;
                }

                try
                {
                    string wellid = parts[0];
                    double x = double.Parse(parts[1], CultureInfo.InvariantCulture);
                    double y = double.Parse(parts[2], CultureInfo.InvariantCulture);

                    Interval interval = new Interval
                    {
                        DepthFrom = double.Parse(parts[3], CultureInfo.InvariantCulture),
                        DepthTo = double.Parse(parts[4], CultureInfo.InvariantCulture),
                        Rock = parts[5],
                        Porosity = double.Parse(parts[6], CultureInfo.InvariantCulture),
                        RowNumber = row,
                    };

                    if (!wells.ContainsKey(wellid))
                    {
                        wells[wellid] = new Well
                        {
                            WellId = wellid,
                            X = x,
                            Y = y
                        };
                    }

                    wells[wellid].Intervals.Add(interval);
                }
                catch (Exception ex)
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = row,
                        WellId = parts.Length > 0 ? parts[0] : "",
                        ErrorMessage = $"Ошибка парсинга данных ({ex.Message})"
                    });
                }
            }

            return (wells.Values.ToList(), errors);
        }
    }
}
