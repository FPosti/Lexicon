namespace Övning1API.DTOs;

public class ProductStatsDto
{
    public int TotalNumberOfProducts { get; set; }
    public decimal  TotalValue { get; set; }
    public decimal AvgPrice { get; set; }
}