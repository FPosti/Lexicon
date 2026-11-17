namespace Övning1API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Count { get; set; }

        public long InventoryValue => (long)Price * Count;
    }
}
