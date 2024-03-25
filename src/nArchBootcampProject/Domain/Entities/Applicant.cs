using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;

public class Applicant : User
{
    public Applicant()
    {
        ApplicationInformations = new HashSet<ApplicationInformation>();
    }

    public string About { get; set; }

    //public virtual BlackList? BlackList { get; set; }
    public virtual ICollection<ApplicationInformation>? ApplicationInformations { get; set; }
}
