using DefectTracker.Core;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public partial class DefectTrackerDbContext : DbContext
{
    public DefectTrackerDbContext()
    {
    }

    public DbSet<Activities> Activities { get; set; }

    public DbSet<DefectQualifierTypes> DefectQualifierTypes { get; set; }

    public DbSet<DefectReportedByTypes> DefectReportedByTypes { get; set; }

    public DbSet<Defects> Defects { get; set; }

    public DbSet<DefectTypes> DefectTypes { get; set; }

    public DbSet<ProjectAreas> ProjectAreas { get; set; }

    public DbSet<ProjectBugs> ProjectBugs { get; set; }

    public DbSet<Projects> Projects { get; set; }

    public DbSet<ProjectUsers> ProjectUsers { get; set; }

    public DbSet<Tasks> Tasks { get; set; }

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