using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class BlackList : Entity<int>
{
    public string Reason { get; set; }
    public DateTime Date { get; set; }
    public Guid ApplicantId { get; set; }
    public virtual Applicant? Applicant { get; set; }
}
