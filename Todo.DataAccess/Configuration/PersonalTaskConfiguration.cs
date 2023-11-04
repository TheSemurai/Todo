using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.DataAccess.Entities;

namespace Todo.DataAccess.Configuration;

public class PersonalTaskConfiguration : IEntityTypeConfiguration<PersonalTask>
{
    public void Configure(EntityTypeBuilder<PersonalTask> builder)
    {
        throw new NotImplementedException();
    }
}