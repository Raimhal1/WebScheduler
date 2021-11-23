using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL.EntityTypeConfigurations
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(e => e.EventName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.StartEventDate).IsRequired();
            builder.Property(e => e.EndEventDate).IsRequired();
            builder.Property(e => e.ShortDescription).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(2000);
        }

    }
}
