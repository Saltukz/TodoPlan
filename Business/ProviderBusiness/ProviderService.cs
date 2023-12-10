using AutoMapper;
using Azure;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public class ProviderService : IProviderService
{
    private readonly ICalculateFactory _calculateFactory;
    private readonly IMapper _mapper;

    public ProviderService(ICalculateFactory calculateFactory, IMapper mapper)
    {
        _calculateFactory = calculateFactory;
        _mapper = mapper;
    }

    async Task<TodoResponse<ResponseSaveProject>> IProviderService.SaveAsync(RequestProjectModel request)
    {
        var result = new TodoResponse<ResponseSaveProject>();

        try
        {
            Project project = _mapper.Map<Project>(request);

            List<Developer> developers = _mapper.Map<List<Developer>>(request.Developers);

            List<ProjectTask> tasks = _mapper.Map<List<ProjectTask>>(request.Tasks);

            ResponseSaveProject response = await _calculateFactory.SaveProject(project, developers, tasks);
            result.IsSuccess = response.IsSuccess;
            result.ErrorMessage = response.ErrorMessage;

            await _calculateFactory.OrganizeSchedule(response.Project.IdProject);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.ErrorMessage = e.Message;
        }

        return result;
    }
}