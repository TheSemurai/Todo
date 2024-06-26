using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Configuration;
using Todo.DataAccess.Entities;

namespace Todo.DataAccess;

public class ApplicationContext : IdentityDbContext<
    User, 
    Role, 
    long>
{
    public DbSet<PersonalTask> Tasks { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected void OnConfiguring(DbContextOptionsBuilder<ApplicationContext> optionsBuilder)
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