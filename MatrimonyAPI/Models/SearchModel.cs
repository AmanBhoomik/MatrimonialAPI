namespace MatrimonyAPI.Models
{
    public class SearchModel
    {
        public string? Gender { get; set; }
        public int? AgeStart { get; set; }
        public int? AgeEnd { get; set; }
        public string? Diet { get; set; }

        public string? City { get; set; }
    }
}
