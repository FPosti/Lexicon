using Microsoft.EntityFrameworkCore;

public class StorageContext(DbContextOptions<StorageContext> options) : DbContext(options)
{
    public DbSet<Övning1API.Models.Product> Product { get; set; } = default!;
}
