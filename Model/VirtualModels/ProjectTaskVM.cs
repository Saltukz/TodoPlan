using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class ProjectTaskVM
{
    public int IdTask { get; set; }

    public int IdProject { get; set; }

    public int? IdDeveloper { get; set; }

    public string Name { get; set; } = null!;

    public int Difficulty { get; set; }

    public int Duration { get; set; }
}