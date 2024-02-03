using Microsoft.EntityFrameworkCore;
using PassMaui.Domain;
using PassMaui.Infrastructure.Accounts.EntityTypeConfiguration;

namespace PassMaui.Infrastructure;

public class ApplicationContext : DbContext
{
    public const string SCHEMA = "MasterData"; 

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PassMaui;Trusted_Connection=True;Encrypt=False;");
        }
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
    }
}
