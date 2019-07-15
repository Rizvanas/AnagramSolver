using Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class CachedWordsConfiguration : IEntityTypeConfiguration<CachedWordEntity>
    {
        public void Configure(EntityTypeBuilder<CachedWordEntity> builder)
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
