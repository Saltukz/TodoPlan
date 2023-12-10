using System;
using System.Collections.Generic;

namespace ToDo_Planning.Entities;

public partial class Project
{
    public int IdProject { get; set; }

    public string ProjectName { get; set; } = null!;

    public string ProviderUrl { get; set; } = null!;
}
