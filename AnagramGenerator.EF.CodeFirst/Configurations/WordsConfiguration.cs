using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class WordsConfiguration : IEntityTypeConfiguration<WordEntity>
    {
        public void Configure(EntityTypeBuilder<WordEntity> builder)
        {
            builder.HasKey(e => e.WordId);

            builder.HasIndex(e => e.Word)
                .IsUnique();

            builder.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
