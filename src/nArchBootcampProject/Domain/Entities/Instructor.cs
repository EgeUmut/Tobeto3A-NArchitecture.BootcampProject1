using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Instructor : User
{
    public string CompanyName { get; set; }
    //public virtual ICollection<Bootcamp>? Bootcamps { get; set; }
}
