using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ApplicationInformationConfiguration : IEntityTypeConfiguration<ApplicationInformation>
{
    public void Configure(EntityTypeBuilder<ApplicationInformation> builder)
    {
        builder.ToTable("ApplicationInformations");

        builder.Property(ai => ai.Id).HasColumnName("Id").IsRequired();
        builder.Property(ai => ai.ApplicantId).HasColumnName("ApplicantId");
        builder.Property(ai => ai.BootcampId).HasColumnName("BootcampId");
        builder.Property(ai => ai.ApplicationStateId).HasColumnName("ApplicationStateId");
        builder.Property(ai => ai.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ai => ai.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ai => ai.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ai => !ai.DeletedDate.HasValue);

        builder.HasOne(p => p.Bootcamp);
        builder.HasOne(p => p.ApplicationStateInformations);
        builder.HasOne(p => p.Applicant);
    }
}
