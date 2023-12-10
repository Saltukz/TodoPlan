using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class RequestProjectModel
{
    public string ProjectName { get; set; }
    public string ProviderUrl { get; set; }
    public List<RequestDeveloper>? Developers { get; set; }
    public List<RequestProjectTask>? Tasks { get; set; }
}

public class RequestDeveloper
{
    public string? Name { get; set; }
    public int? Capacity { get; set; }
}

public class RequestProjectTask
{
    public string? Name { get; set; }
    public int? Duration { get; set; }
    public int? Difficulty { get; set; }
}