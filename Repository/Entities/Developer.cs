using System;
using System.Collections.Generic;

namespace ToDo_Planning.Entities;

public partial class Developer
{
    public int IdDeveloper { get; set; }

    public int IdProject { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }
}
