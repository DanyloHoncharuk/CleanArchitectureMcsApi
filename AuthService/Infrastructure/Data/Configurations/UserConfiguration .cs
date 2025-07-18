﻿using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(u => u.Email)
                .HasMaxLength(100);
        }
    }
}
