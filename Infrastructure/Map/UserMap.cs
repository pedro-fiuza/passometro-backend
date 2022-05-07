using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(128);

            builder.Property(x => x.Crm)
                .HasColumnName("Crm")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            builder.Property(x => x.PasswordSalt)
                .IsRequired()
                .HasColumnName("PasswordSalt")
                .HasColumnType("bytea");


            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("bytea");

            builder.Property(x => x.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate")
                .HasColumnType("timestamp");

            builder.HasMany(p => p.Resumes)
                   .WithMany(h => h.Doctors)
                   .UsingEntity<Dictionary<string, object>>
                   (
                       "UserResume",
                       user => user.HasOne<Resume>()
                           .WithMany()
                           .HasForeignKey("ResumeId")
                           .HasConstraintName("FK_UserResume_ResumeId")
                           .OnDelete(DeleteBehavior.Cascade),
                       resume => resume.HasOne<User>()
                           .WithMany()
                           .HasForeignKey("UserId")
                           .HasConstraintName("FK_UserResume_UserId")
                           .OnDelete((DeleteBehavior.Cascade))
                   );
        }
    }
}
