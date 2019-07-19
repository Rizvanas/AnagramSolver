using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLogEntity>
    {
        public void Configure(EntityTypeBuilder<UserLogEntity> builder)
        {
            builder.Property(e => e.UserIp)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.HasOne(d => d.SearchPhrase)
                .WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.SearchPhraseId);
        }
    }
}
