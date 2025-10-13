namespace asp03receiptsAPI.DTOs
{
    public class ListItems<T>
    {
        public int Page { get; set; }
        public int? Size { get; set; }
        public required int TotalCount { get; set; }
        public required List<T> Items { get; set; }
    }
}
