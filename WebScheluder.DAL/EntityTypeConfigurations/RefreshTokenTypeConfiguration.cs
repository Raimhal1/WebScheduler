﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            builder.ToTable("RefreshTokens").HasKey(t => t.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.HasOne(t => t.User).WithMany(p => p.RefreshTokens);
        }
    }
}
