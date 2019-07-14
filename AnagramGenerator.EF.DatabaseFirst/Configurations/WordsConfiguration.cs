using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class WordsConfiguration : IEntityTypeConfiguration<Words>
    {
        public void Configure(EntityTypeBuilder<Words> builder)
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
