using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasIndex(e => e.Ip)
                .IsUnique();

            builder.Property(e => e.Ip)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.SearchesLeft)
                .IsRequired()
                .HasDefaultValue(5);
        }
    }
}
