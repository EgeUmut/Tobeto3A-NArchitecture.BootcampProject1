﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class BootcampImage : Entity<int>
{
    public int BootcampId { get; set; }
    public string ImagePath { get; set; }

    public virtual Bootcamp? Bootcamp { get; set; }

    public BootcampImage() { }
    //public BootcampImage(Guid bootcampId, string ımagePath, Bootcamp bootcamp)
    //{ BootcampId = bootcampId; ImagePath = ımagePath; Bootcamp = bootcamp; }
}
