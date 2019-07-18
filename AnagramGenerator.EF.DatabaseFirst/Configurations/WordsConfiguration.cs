using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class WordsConfiguration : IEntityTypeConfiguration<WordEntity>
    {
        public void Configure(EntityTypeBuilder<WordEntity> builder)
        {
            builder.HasKey(e => e.WordId);

            builder.HasIndex(e => e.Word)
                .HasName("IX_Words")
                .IsUnique();

            builder.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
