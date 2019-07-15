﻿using Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Configurations
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
                .WithMany(p => p.UserLog)
                .HasForeignKey(d => d.SearchPhraseId)
                .HasConstraintName("FK_UserLog_Phrases");
        }
    }
}
