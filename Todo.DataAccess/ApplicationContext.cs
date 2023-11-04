using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Configuration;
using Todo.DataAccess.Entities;

namespace Todo.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<PersonalTask> Tasks { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.ApplyConfiguration(new PersonalTaskConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}