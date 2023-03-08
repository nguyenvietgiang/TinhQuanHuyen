namespace BaitapDemo.Models
{
    public class TimezoneQueryModel
    {
        public string FullTextSearch { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string Sort { get; set; }
    }
}
