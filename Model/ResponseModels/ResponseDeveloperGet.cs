using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class ResponseDeveloperGet
{
    public int IdTask { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public string title { get; set; }
}