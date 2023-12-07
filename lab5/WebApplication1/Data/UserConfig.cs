using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public class UserConfig : IEntityTypeConfiguration<L5User>
{
    public void Configure(EntityTypeBuilder<L5User> builder)
    {
        builder.HasKey(appUser => appUser.Id);
    }
}