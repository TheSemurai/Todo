using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.DataAccess.Entities;

namespace Todo.DataAccess.Configuration;

public class PersonalTaskConfiguration : IEntityTypeConfiguration<PersonalTask>
{
    public void Configure(EntityTypeBuilder<PersonalTask> builder)
    {
        builder
            .HasKey(k => k.Id);

        builder
            .Property(p => p.Title)
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(p => p.Content)
            .HasMaxLength(900)
            .IsRequired();

        builder
            .Property(e => e.IsComplete)
            .IsRequired();

        builder 
            .HasOne(one => one.Author)
            .WithMany(many => many.Tasks)
            .OnDelete(DeleteBehavior.Cascade);
    }
}