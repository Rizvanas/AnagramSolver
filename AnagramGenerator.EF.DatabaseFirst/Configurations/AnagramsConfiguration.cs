using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class AnagramsConfiguration : IEntityTypeConfiguration<AnagramEntity>
    {
        public void Configure(EntityTypeBuilder<AnagramEntity> builder)
        {
            builder.HasIndex(e => e.Anagram)
                    .HasName("IX_Anagrams")
                    .IsUnique();

            builder.Property(e => e.Anagram)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
