using System.ComponentModel.DataAnnotations;

namespace sunstealer.mvc.odata.Models
{
    public class Dictionary1
    {
        public Dictionary<string, object> Dictionary { get; set; } = new();
    }

    public class Model1
    {
        [Key]
        public int Key { get; set; } = -1;
        public string Value { get; set; } = string.Empty;

        public Dictionary1 Dictionary { get; set; } = new();
        public List<ListEntry> List { get; set; } = new();
    }

    public class ListEntry
    {
        [Key]
        public int Key { get; set; } = -1;
        public string Value { get; set; } = string.Empty;
    }
}
