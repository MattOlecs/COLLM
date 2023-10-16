using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Contexts;

public class DesignTimeContext : IDesignTimeDbContextFactory<LmacoContext>
{
    public LmacoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LmacoContext>();
        optionsBuilder.UseNpgsql(
            "User ID=admin;Password=1111;Host=localhost;Port=5432;Database=LMACO;Pooling=true;");

        return new LmacoContext(optionsBuilder.Options);
    }
}