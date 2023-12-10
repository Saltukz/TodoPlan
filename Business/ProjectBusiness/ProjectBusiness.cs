using AutoMapper;
using Azure;
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

public class ProjectBusiness : IProjectBusiness
{
    private readonly ToDoPlanningContext _context;
    private readonly IMapper _mapper;

    public ProjectBusiness(ToDoPlanningContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<TodoResponse<ResponseProjectGet>> Get(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<TodoResponse<ResponseProjectGetAll>> GetAll()
    {
        var result = new TodoResponse<ResponseProjectGetAll>();

        try
        {
            result.DataList = _mapper.Map<List<ResponseProjectGetAll>>(await _context.Projects.ToListAsync());
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