using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class UserWordsConfiguration : IEntityTypeConfiguration<UserWordEntity>
    {
        public void Configure(EntityTypeBuilder<UserWordEntity> builder)
        {
            builder.HasIndex(e => e.Word)
                .HasName("IX_UserWords")
    .           IsUnique();

            builder.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserWords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserWords_Users");
        }
    }
}
