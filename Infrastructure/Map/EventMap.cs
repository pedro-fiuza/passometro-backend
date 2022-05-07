using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("varchar")
                .HasMaxLength(450);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("text");

            builder.Property(x => x.EventDate)
                .IsRequired()
                .HasColumnName("EventDate")
                .HasColumnType("timestamp");

            builder.HasOne(h => h.Resume)
               .WithMany(e => e.Events)
               .HasConstraintName("FK_Event_Resume")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
