using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class PhrasesConfiguration : IEntityTypeConfiguration<PhraseEntity>
    {
        public void Configure(EntityTypeBuilder<PhraseEntity> builder)
        {
            builder.HasIndex(e => e.Phrase)
                .IsUnique();

            builder.Property(e => e.Phrase)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
