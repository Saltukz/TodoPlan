using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Model;

public class ResponseSaveProject
{
    public bool IsSuccess = true;
    public string ErrorMessage { get; set; }
    public Project Project { get; set; }
}