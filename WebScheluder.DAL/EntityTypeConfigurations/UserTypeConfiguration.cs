using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
            builder.HasIndex(p => new { p.Id, p.Email }).IsUnique();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
