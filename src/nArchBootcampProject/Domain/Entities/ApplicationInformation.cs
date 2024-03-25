using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ApplicationInformation : Entity<int>
{
    public Guid ApplicantId { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public int BootcampId { get; set; }
    public virtual Bootcamp? Bootcamp { get; set; }
    public short ApplicationStateId { get; set; }
    public virtual ApplicationStateInformation? ApplicationStateInformations { get; set; }
}
