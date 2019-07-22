using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasIndex(e => e.Ip)
                .HasName("IX_Users")
                .IsUnique();

            builder.Property(e => e.Ip)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.SearchesLeft)
                .IsRequired()
                .HasDefaultValue(5);
        }
    }
}
