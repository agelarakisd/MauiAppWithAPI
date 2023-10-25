using Microsoft.EntityFrameworkCore;

namespace PassMaui.Infrastructure;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(".\\SQLEXPRESS;Database=PassMaui;Trusted_Connection=True;");
        }
    }
}
