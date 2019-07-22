using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLogEntity>
    {
        public void Configure(EntityTypeBuilder<UserLogEntity> builder)
        {
            builder.HasOne(d => d.Anagram)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.AnagramId);

            builder.HasOne(d => d.Phrase)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.PhraseId);

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
