namespace Core.Models
{
    public class ValidationError
    {
        public int RowNumber { get; set; }
        public string WellId { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}
