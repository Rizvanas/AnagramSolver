using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class AnagramsConfiguration : IEntityTypeConfiguration<AnagramEntity>
    {
        public void Configure(EntityTypeBuilder<AnagramEntity> builder)
        {
            builder.HasIndex(e => e.Anagram)
                .IsUnique();

            builder.Property(e => e.Anagram)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
