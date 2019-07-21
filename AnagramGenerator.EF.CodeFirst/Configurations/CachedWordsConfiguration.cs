using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class CachedWordsConfiguration : IEntityTypeConfiguration<CachedWordEntity>
    {
        public void Configure(EntityTypeBuilder<CachedWordEntity> builder)
        {
            builder.HasOne(d => d.Anagram)
                .WithMany(p => p.CachedWords)
                .HasForeignKey(d => d.AnagramId);

            builder.HasOne(d => d.Phrase)
                .WithMany(p => p.CachedWords)
                .HasForeignKey(d => d.PhraseId);
        }
    }
}
