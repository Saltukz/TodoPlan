using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public interface ICalculateFactory
{
    Task<decimal?> CalculateProjectTime(int id);
    Task OrganizeSchedule(int idProject);

    Task<ResponseSaveProject> SaveProject(Project project, List<Developer> developers, List<ProjectTask> tasks);
}