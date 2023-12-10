using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class DeveloperVM
{
    public int IdDeveloper { get; set; }

    public int IdProject { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public bool IsGotTask { get; set; }
}