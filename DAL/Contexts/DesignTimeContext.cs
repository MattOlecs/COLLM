using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Contexts;

public class DesignTimeContext : IDesignTimeDbContextFactory<CollmContext>
{
    public CollmContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CollmContext>();
        optionsBuilder.UseNpgsql(
            "User ID=admin;Password=1111;Host=localhost;Port=5432;Database=COLLM;Pooling=true;");

        return new CollmContext(optionsBuilder.Options);
    }
}