using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLogEntity>
    {
        public void Configure(EntityTypeBuilder<UserLogEntity> builder)
        {
            builder.HasOne(d => d.Anagram)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.AnagramId)
                .HasConstraintName("FK_UserLogs_Anagrams");

            builder.HasOne(d => d.Phrase)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.PhraseId)
                .HasConstraintName("FK_UserLogs_Phrases");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLogs_Users");
        }
    }
}
