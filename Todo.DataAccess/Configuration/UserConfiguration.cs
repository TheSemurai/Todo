using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.DataAccess.Entities;

namespace Todo.DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(k => k.Id);
        
        builder
            .Property(p => p.Name)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(p => p.Lastname)
            .HasMaxLength(25)
            .IsRequired();
        
        builder
            .Property(p => p.UserName)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .HasIndex(i => i.UserName)
            .IsUnique();

        builder
            .Property(p => p.PasswordHash)
            .IsRequired();

        builder
            .Property(e => e.Email)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(e => e.Gender)
            .HasConversion<string>();
    }
}