using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class AnagramsConfiguration : IEntityTypeConfiguration<Anagrams>
    {
        public void Configure(EntityTypeBuilder<Anagrams> builder)
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
