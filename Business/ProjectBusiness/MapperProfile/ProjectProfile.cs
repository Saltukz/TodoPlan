using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ResponseProjectGetAll, Project>().ReverseMap();
        CreateMap<ResponseProjectGet, Project>().ReverseMap();
    }
}