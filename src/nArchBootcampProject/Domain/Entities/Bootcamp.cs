using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Bootcamp : Entity<int>
{
    public Bootcamp()
    {
        ApplicationInformations = new HashSet<ApplicationInformation>();
    }

    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public virtual Instructor? Instructor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public short BootcampStateId { get; set; }
    public virtual BootcampState? BootcampStates { get; set; }
    public virtual ICollection<ApplicationInformation>? ApplicationInformations { get; set; }

    //public virtual ICollection<BootcampImage> BootcampImages { get; set; }
    public int BootcampImageId { get; set; }
    public BootcampImage BootcampImage { get; set; }
}
