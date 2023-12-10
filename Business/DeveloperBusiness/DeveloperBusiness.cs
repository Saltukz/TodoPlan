using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public class DeveloperBusiness : IDeveloperBusiness
{
    private readonly ToDoPlanningContext _context;
    private readonly IMapper _mapper;

    public DeveloperBusiness(ToDoPlanningContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodoResponse<ResponseDeveloperGet>> Get(int Id)
    {
        var result = new TodoResponse<ResponseDeveloperGet>();

        try
        {
            var query = from cr in _context.DeveloperProjectTaskCrs
                        join t in _context.ProjectTasks on cr.IdTask equals t.IdTask
                        select new ResponseDeveloperGet
                        {
                            start = (DateTime)cr.StartTime,
                            end = (DateTime)cr.EndTime,
                            IdTask = cr.IdTask,
                            title = t.Name
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