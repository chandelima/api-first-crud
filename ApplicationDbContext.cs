using api_first_crud.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

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
        builder.Entity<Category>()
            .ToTable("Categories");
    }
}