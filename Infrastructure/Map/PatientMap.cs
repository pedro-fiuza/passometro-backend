using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(x => x.Age)
                .IsRequired()
                .HasColumnName("Age")
                .HasColumnType("int4");

            builder.Property(x => x.MotherName)
                .IsRequired()
                .HasColumnName("MotherName")
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnName("BirthDate")
                .HasColumnType("timestamp");
        }
    }
}
