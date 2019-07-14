using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class PhrasesConfiguration : IEntityTypeConfiguration<Phrases>
    {
        public void Configure(EntityTypeBuilder<Phrases> builder)
        {
            builder.HasIndex(e => e.Phrase)
                .HasName("IX_Phrases")
                .IsUnique();

            builder.Property(e => e.Phrase)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
