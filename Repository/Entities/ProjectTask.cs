using System;
using System.Collections.Generic;

namespace ToDo_Planning.Entities;

public partial class ProjectTask
{
    public int IdTask { get; set; }

    public int IdProject { get; set; }

    public int? IdDeveloper { get; set; }

    public string Name { get; set; } = null!;

    public int Difficulty { get; set; }

    public int Duration { get; set; }
}
