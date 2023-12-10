using System;
using System.Collections.Generic;

namespace ToDo_Planning.Entities;

public partial class DeveloperProjectTaskCr
{
    public int IdDeveloperTaskCr { get; set; }

    public int IdDeveloper { get; set; }

    public int IdTask { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}
