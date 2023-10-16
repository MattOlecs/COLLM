using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

public class LmacoContext : DbContext
{
    public LmacoContext(DbContextOptions<LmacoContext> options): base(options)
    {
        
    }

    public LmacoContext()
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