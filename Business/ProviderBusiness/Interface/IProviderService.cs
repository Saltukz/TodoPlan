using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business;

public interface IProviderService
{
    Task<TodoResponse<ResponseSaveProject>> SaveAsync(RequestProjectModel request);
}