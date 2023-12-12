using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public class ProjectBusiness : IProjectBusiness
{
    private readonly ToDoPlanningContext _context;
    private readonly IMapper _mapper;
    private readonly ICalculateFactory _calculateFactory;

    public ProjectBusiness(ToDoPlanningContext context, IMapper mapper, ICalculateFactory calculateFactory)
    {
        _context = context;
        _mapper = mapper;
        _calculateFactory = calculateFactory;
    }

    public async Task<TodoResponse<ResponseProjectGet>> Get(int Id)
    {
        var result = new TodoResponse<ResponseProjectGet>();

        try
        {
            var query = from p in _context.Projects
                        join d in _context.Developers on p.IdProject equals d.IdProject
                        where p.IdProject == Id
                        group d by new { p.ProjectName } into grouped
                        select new ResponseProjectGet
                        {
                            Name = grouped.Key.ProjectName,
                            lstDeveloper = grouped.Select(dev => new DeveloperVM
                            {
                                IdProject = Id,
                                Capacity = dev.Capacity,
                                IdDeveloper = dev.IdDeveloper,
                                Name = dev.Name
                            }).ToList()
                        };

            var response = await query.FirstOrDefaultAsync();
            response.Time = await _calculateFactory.CalculateProjectTime(Id);
            result.Data = response;
            return result;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
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