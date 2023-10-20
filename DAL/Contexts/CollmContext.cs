using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

public class CollmContext : DbContext
{
    public CollmContext(DbContextOptions<CollmContext> options): base(options)
    {
        
    }

    public CollmContext()
    {
        
    }
    
    public DbSet<Request> Requests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Request>()
            .HasKey(r => r.ID);
    }
}