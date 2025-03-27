using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserService.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(u => u.Email)
                .HasMaxLength(100);
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(100);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(u => u.DateOfBirth)
                .IsRequired();
        }
    }
}
