using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class ReportTypeConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports").HasKey(r => r.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.HasOne(r => r.User).WithMany(u => u.Reports);
        }
    }
}
