using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class PhrasesConfiguration : IEntityTypeConfiguration<PhraseEntity>
    {
        public void Configure(EntityTypeBuilder<PhraseEntity> builder)
        {
            builder.HasIndex(e => e.Phrase)
                .HasName("IX_Phrase")
                .IsUnique();

            builder.Property(e => e.Phrase)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
