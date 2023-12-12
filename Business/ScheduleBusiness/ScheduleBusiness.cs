using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using ToDo_Planning.Entities;
using ToDo_Planning.Model;

namespace ToDo_Planning.Business;

public class ScheduleBusiness : IScheduleBusiness
{
    private readonly ToDoPlanningContext _context;
    private readonly IMapper _mapper;

    public ScheduleBusiness(ToDoPlanningContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodoResponse<ResponseScheduleGet>> Get(int Id)
    {
        var result = new TodoResponse<ResponseScheduleGet>();

        try
        {
            var query = from cr in _context.DeveloperProjectTaskCrs
                        join tsk in _context.ProjectTasks on cr.IdTask equals tsk.IdTask
                        where cr.IdDeveloper == Id
                        orderby cr.StartTime
                        select new ResponseScheduleGet
                        {
                            TaskID = cr.IdTask,
                            Start = cr.StartTime,
                            End = cr.EndTime,
                            Title = tsk.Name
                        };

            result.DataList = await query.ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}