using ICS_project.DAL.Entities;
using ICS_project.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL;

public class ICS_projectDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public ICS_projectDbContext(DbContextOptions contextOptions, bool seedDemoData = true)
         : base(contextOptions) =>
         _seedDemoData = seedDemoData;

    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

    public DbSet<ProjectUserEntity> ProjectAmounts => Set<ProjectUserEntity>();

    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

    public DbSet<TagActivityEntity> TagAmounts => Set<TagActivityEntity>();

    public DbSet<TagEntity> Tag => Set<TagEntity>();

    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite($"Data Source=SQLite.db;Cache=Shared");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ActivityEntity>()
            .HasMany<TagActivityEntity>(e => e.Tags)
            .WithOne(e => e.Activity)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectEntity>()
            .HasMany<ActivityEntity>(e => e.Activities)
            .WithOne(e => e.Project)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<ActivityEntity>()
            .HasOne<UserEntity>(e => e.User)
            .WithMany(e => e.Activities)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ActivityEntity>()
            .HasOne<ProjectEntity>(e => e.Project)
            .WithMany(e => e.Activities)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserEntity>()
            .HasMany<ActivityEntity>(e => e.Activities)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectEntity>()
            .HasMany<ProjectUserEntity>(e => e.Users)
            .WithOne(e => e.Project)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TagEntity>()
            .HasMany<TagActivityEntity>(e=>e.Activities)
            .WithOne(e => e.Tag)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserEntity>()
            .HasMany<ProjectUserEntity>(e => e.Projects)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Cascade);

        if (!_seedDemoData) return;

        UserSeeds.Seed(modelBuilder);
        ProjectSeeds.Seed(modelBuilder);
        ProjectUserSeeds.Seed(modelBuilder);
        TagSeeds.Seed(modelBuilder);
        ActivitySeeds.Seed(modelBuilder);
        TagActivitySeeds.Seed(modelBuilder);
    }
}
