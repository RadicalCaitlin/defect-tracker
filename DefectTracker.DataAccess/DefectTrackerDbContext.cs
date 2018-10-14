using DefectTracker.Core;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public partial class DefectTrackerDbContext : DbContext
{
    public DefectTrackerDbContext()
    {
    }

    public DbSet<Projects> Projects { get; set; }

    public DefectTrackerDbContext(DbContextOptions<DefectTrackerDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}