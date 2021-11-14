using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class AllowedFileTypeTypeConfiguration : IEntityTypeConfiguration<AllowedFileType>
    {
        public void Configure(EntityTypeBuilder<AllowedFileType> builder)
        {
            builder.ToTable("AllowedFileTypes").HasKey(ef => ef.Id);
            builder.HasIndex(e => e.Id).IsUnique();
        }
    }
}
