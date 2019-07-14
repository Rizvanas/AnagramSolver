using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class CachedWordsConfiguration : IEntityTypeConfiguration<CachedWords>
    {
        public void Configure(EntityTypeBuilder<CachedWords> builder)
        {
            builder.HasIndex(e => e.PhraseId)
                .HasName("IX_CachedWords");

            builder.HasOne(d => d.Anagram)
                .WithMany(p => p.CachedWords)
                .HasForeignKey(d => d.AnagramId)
                .HasConstraintName("FK_CachedWords_Anagrams");

            builder.HasOne(d => d.Phrase)
                .WithMany(p => p.CachedWords)
                .HasForeignKey(d => d.PhraseId)
                .HasConstraintName("FK_CachedWords_Phrases");
        }
    }
}
