using Model;
using ToDo_Planning.Model;

namespace ToDo_Planning.Business;

public interface IScheduleBusiness
{
    Task<TodoResponse<ResponseScheduleGet>> Get(int Id);
}