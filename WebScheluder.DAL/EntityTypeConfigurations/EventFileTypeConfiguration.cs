using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class EventFileTypeConfiguration : IEntityTypeConfiguration<EventFile>
    {
        public void Configure(EntityTypeBuilder<EventFile> builder)
        {
            builder.ToTable("EventFiles").HasKey(ef => ef.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.HasOne(ef => ef.Event).WithMany(e => e.EventFiles).HasForeignKey(ef => ef.EventId);
        }
    }
}
