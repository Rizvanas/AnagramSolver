using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnagramGenerator.EF.CodeFirst.Configurations
{
    public class UserWordsConfiguration : IEntityTypeConfiguration<UserWordEntity>
    {
        public void Configure(EntityTypeBuilder<UserWordEntity> builder)
        {
            builder.HasIndex(e => e.Word)
    .           IsUnique();

            builder.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserWords)
                .HasForeignKey(d => d.UserId);
        }
    }
}
