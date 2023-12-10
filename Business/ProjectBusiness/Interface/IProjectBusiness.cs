using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business;

public interface IProjectBusiness
{
    Task<TodoResponse<ResponseProjectGetAll>> GetAll();

    Task<TodoResponse<ResponseProjectGet>> Get(int Id);
}