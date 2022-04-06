using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    //Applying FluentAPI for columns properties
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .Property(p => p.Code)
            .HasMaxLength(20)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(120)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(500)
            .IsRequired(false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //String Connection
        options.UseSqlServer(
            "Server=localhost;Database=Products;User Id=sa;Password=Aaaa1111;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES"
        );
    }
}