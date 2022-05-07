using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class HospitalMap : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.ToTable("Hospital");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.City)
                .IsRequired()
                .HasColumnName("City")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Uf)
                .IsRequired()
                .HasColumnName("Uf")
                .HasColumnType("VARCHAR")
                .HasMaxLength(5);

            builder.HasIndex(x => x.Name, "IX_Hospital_Name");

            builder.HasMany(p => p.Patients)
                   .WithMany(h => h.Hospitals)
                   .UsingEntity<Dictionary<string, object>>
                   (
                       "HospitalPatient",
                       hospital => hospital.HasOne<Patient>()
                           .WithMany()
                           .HasForeignKey("PatientId")
                           .HasConstraintName("FK_HospitalPatient_PatientId")
                           .OnDelete(DeleteBehavior.Cascade),
                       patient => patient.HasOne<Hospital>()
                           .WithMany()
                           .HasForeignKey("HospitalId")
                           .HasConstraintName("FK_HospitalPatient_HospitalId")
                           .OnDelete((DeleteBehavior.Cascade))
                   );
        }
    }
}
