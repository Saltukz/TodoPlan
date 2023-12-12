using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class ResponseProjectGet
{
    public string? Name { get; set; }
    public decimal? Time { get; set; }
    public List<DeveloperVM>? lstDeveloper { get; set; }
}