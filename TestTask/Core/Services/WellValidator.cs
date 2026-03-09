using Core.Models;

namespace Core.Services
{
    public class WellValidator
    {
        public List<ValidationError> Validate(List<Well> wells)
        {
            var errors = new List<ValidationError>();

            foreach(var well in wells)
            {
                var intervals = well.Intervals.OrderBy(i => i.DepthFrom).ToList();

                for (int i = 0; i < intervals.Count; i++)
                {
                    var current = intervals[i];

                    if(current.DepthFrom >= current.DepthTo)
                    {
                        errors.Add(new ValidationError
                        {
                            WellId = well.WellId,
                            ErrorMessage = "DepthFrom должно быть строго меньше DepthTo"
                        });
                    }

                    if(current.DepthFrom < 0)
                    {
                        errors.Add(new ValidationError
                        {
                            WellId = well.WellId,
                            ErrorMessage = "DepthFrom должен быть неотрицательным"
                        });
                    }

                    if(current.Porosity < 0 || current.Porosity > 1)
                    {
                        errors.Add(new ValidationError
                        {
                            WellId = well.WellId,
                            ErrorMessage = "Porosity должно находиться в диапазоне [0..1]"
                        });
                    }

                    if (string.IsNullOrWhiteSpace(current.Rock))
                    {
                        errors.Add(new ValidationError
                        {
                            WellId = well.WellId,
                            ErrorMessage = "Rock не должен быть пустым"
                        });
                    }

                    if (i > 0)
                    {
                        var prev = intervals[i - 1];

                        if(current.DepthFrom < prev.DepthTo)
                        {
                            errors.Add(new ValidationError
                            {
                                WellId = well.WellId,
                                ErrorMessage = "Интервалы по одной скважине не должны пересекаться"
                            });
                        }
                    }
                }
            }

            return errors;
        }
    }
}
