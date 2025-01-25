using Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Table>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<Table>()
            .HasMany(t => t.Items)
            .WithOne()
            .HasForeignKey(o => o.TableId);

        modelBuilder.Entity<OrderDetail>()
            .HasMany(o => o.Itens)
            .WithOne();

        modelBuilder.Entity<Item>()
            .HasMany(i => i.Additions)
            .WithOne();

        modelBuilder.Entity<Item>()
            .HasMany(i => i.Options)
            .WithOne();
    }
}