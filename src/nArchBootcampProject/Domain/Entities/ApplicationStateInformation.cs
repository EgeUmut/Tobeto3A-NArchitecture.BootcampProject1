﻿using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ApplicationStateInformation : Entity<short>
{
    public string Name { get; set; }
}
