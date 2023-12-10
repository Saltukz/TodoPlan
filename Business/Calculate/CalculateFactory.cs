using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_Planning.Entities;

namespace Business;

public class CalculateFactory : ICalculateFactory
{
    private readonly ToDoPlanningContext _context;
    private readonly IMapper _mapper;

    public CalculateFactory(ToDoPlanningContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseSaveProject> SaveProject(Project project, List<Developer> developers, List<ProjectTask> tasks)
    {
        //tüm datayı kayıt ettiğim fonksiyon
        var result = new ResponseSaveProject();
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var insertedProject = await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                foreach (var developer in developers)
                {
                    developer.IdProject = insertedProject.Entity.IdProject;
                    await _context.Developers.AddAsync(developer);
                    await _context.SaveChangesAsync();
                }

                foreach (var task in tasks)
                {
                    task.IdProject = insertedProject.Entity.IdProject;
                    await _context.ProjectTasks.AddAsync(task);
                    await _context.SaveChangesAsync();
                }
                result.Project = project;
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ErrorMessage = e.Message;
                await transaction.RollbackAsync();
            }
        }
        return result;
    }

    public async Task OrganizeSchedule(int idProject)
    {
        // haftalık çalışma takvimini organize ettiğim fonksiyon

        var lstdeveloper = await _context.Developers.Where(x => x.IdProject == idProject).ToListAsync();

        var lstTask = await _context.ProjectTasks.Where(x => x.IdProject == idProject).ToListAsync();

        //görev almış developerların idleri
        var lstTaskDeveloper = await _context.DeveloperProjectTaskCrs.Select(x => x.IdDeveloper).ToListAsync();

        foreach (var task in lstTask)
        {
            var taskDeveloperCR = await _context.DeveloperProjectTaskCrs.Where(x => x.IdTask == task.IdTask).FirstOrDefaultAsync();

            //kaydedilecek obje
            var developerProjectTaskCr = new DeveloperProjectTaskCr();

            //obje boşsa task ele alınmamış atama işlemlerine geçebiliriz
            if (taskDeveloperCR == null)
            {
                //task almayan developer kalmasın sonradan ikinci taskları vermeye geçelim
                var availableDevelopers = lstdeveloper.Where(x => x.Capacity == task.Difficulty).Where(a => !lstTaskDeveloper.Contains(a.IdDeveloper)).ToList();

                developerProjectTaskCr.IdTask = task.IdTask;
                //iş almamış developera atıyorum
                if (availableDevelopers.Any())
                {
                    developerProjectTaskCr.IdDeveloper = availableDevelopers.First().IdDeveloper;
                    DateTime currentDate = DateTime.Now;

                    // Calculate the first day of the week
                    DateTime firstDayOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

                    // Set the time to 09:00 AM
                    DateTime startTime = new DateTime(firstDayOfWeek.Year, firstDayOfWeek.Month, firstDayOfWeek.Day, 9, 0, 0);

                    developerProjectTaskCr.StartTime = startTime;
                    developerProjectTaskCr.EndTime = FindDecentTime(startTime, task.Duration);

                    //developerProjectTaskCr.EndTime = startTime.AddHours(task.Duration);
                    await _context.DeveloperProjectTaskCrs.AddAsync(developerProjectTaskCr);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //burayı if else harici tek fonksiyonda bitirebilirmişim sonradan farkettim ama sorun olmaz diyerek bıraktım.
                    FindMostAvailableDeveloperVM objAvailableDeveloperAndItsTime = await FindMostAvailableDeveloper(task.IdTask);

                    if (objAvailableDeveloperAndItsTime != null)
                    {
                        var assignedDeveloper = await _context.Developers.FindAsync(objAvailableDeveloperAndItsTime.IdDeveloper);

                        developerProjectTaskCr.IdDeveloper = objAvailableDeveloperAndItsTime.IdDeveloper;
                        developerProjectTaskCr.IdTask = task.IdTask;
                        developerProjectTaskCr.StartTime = FindDecentTime(objAvailableDeveloperAndItsTime.AvailableTime, task.Duration);
                        developerProjectTaskCr.EndTime = FindDecentTime((DateTime)developerProjectTaskCr.StartTime, task.Duration);
                        await _context.DeveloperProjectTaskCrs.AddAsync(developerProjectTaskCr);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }

    private DateTime? FindDecentTime(DateTime startTime, int duration)
    {
        DateTime endOfDay = new DateTime(startTime.Year, startTime.Month, startTime.Day, 18, 0, 0);

        if (startTime.AddHours(duration) > endOfDay)
        {
            int remainingHours = duration - (int)(endOfDay - startTime).TotalHours;

            DateTime nextDayStartTime = new DateTime(startTime.Year, startTime.Month, startTime.Day + 1, 9, 0, 0);

            if (nextDayStartTime.AddHours(remainingHours).Hour >= 12 && nextDayStartTime.AddHours(remainingHours).Hour < 13)
            {
                remainingHours++;
            }

            return nextDayStartTime.AddHours(remainingHours);
        }
        else
        {
            return startTime.AddHours(duration);
        }
    }

    private async Task<FindMostAvailableDeveloperVM> FindMostAvailableDeveloper(int IdTask)
    {
        var result = new FindMostAvailableDeveloperVM();
        var objTask = await _context.ProjectTasks.FindAsync(IdTask);
        var mostAvailableDevelopers = await (
                 from cr in _context.DeveloperProjectTaskCrs
                 join task in _context.ProjectTasks on cr.IdTask equals task.IdTask
                 join p in _context.Projects on task.IdProject equals p.IdProject
                 group cr by cr.IdDeveloper into grouped
                 orderby grouped.Count()
                 select grouped.Key
                            ).ToListAsync();

        DateTime currentDate = DateTime.Now;

        // Calculate the first day of the week
        DateTime firstDayOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

        // Set the time to 09:00 AM
        DateTime startTime = new DateTime(firstDayOfWeek.Year, firstDayOfWeek.Month, firstDayOfWeek.Day, 9, 0, 0);

        DateTime endTime = startTime.AddDays(4).AddHours(18);

        foreach (var IdDeveloper in mostAvailableDevelopers)
        {
            //bu hafta developerın toplam çalışması
            var totalTask = await _context.DeveloperProjectTaskCrs.Where(x => x.IdDeveloper == IdDeveloper && x.StartTime > startTime && x.EndTime <= endTime).ToListAsync();

            var totalHour = 0;

            foreach (var task in totalTask)
            {
                totalHour += (int)(task.EndTime.Value - task.StartTime.Value).TotalHours;
            }

            if (totalHour + objTask.Duration < 45)
            {
                result.IdDeveloper = IdDeveloper;
                result.AvailableTime = (DateTime)totalTask.OrderByDescending(x => x.EndTime.Value).Select(x => x.EndTime).FirstOrDefault();
                return result;
            }
        }

        return null;
    }
}