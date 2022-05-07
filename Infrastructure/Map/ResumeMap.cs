using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class ResumeMap : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.ToTable("Resume");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Bed)
                .IsRequired()
                .HasColumnName("Bed")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.AdmissionDate)
                .IsRequired()
                .HasColumnName("AdmissionDate")
                .HasColumnType("timestamp");

            builder.Property(x => x.Surgeries)
                .IsRequired()
                .HasColumnName("Surgeries")
                .HasColumnType("text");

            builder.Property(x => x.MainDiagnosis)
                .IsRequired()
                .HasColumnName("MainDiagnosis")
                .HasColumnType("text");

            builder.Property(x => x.Complications)
                .IsRequired()
                .HasColumnName("Complications")
                .HasColumnType("text");

            builder.Property(x => x.ProposalOfTheDay)
                .IsRequired()
                .HasColumnName("ProposalOfTheDay")
                .HasColumnType("text");

            builder.HasOne(p => p.Patient)
                .WithMany(r => r.Resumes)
                .HasConstraintName("FK_Resume_Patient")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
