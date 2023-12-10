using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business.ProviderBusiness.MapperProfile;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        CreateMap<RequestProjectModel, Project>().ReverseMap();
        CreateMap<RequestDeveloper, Developer>().ReverseMap();
        CreateMap<RequestProjectTask, ProjectTask>().ReverseMap();
        CreateMap<DeveloperVM, Developer>().ReverseMap();
        CreateMap<ProjectTaskVM, ProjectTask>().ReverseMap();
        // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
    }
}