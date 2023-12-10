using Business;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ToDo_Planning.Controllers.Provider
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        [HttpGet("[action]")]
        public async Task<TodoResponse<ResponseProjectGetAll>> GetAll() => await _projectBusiness.GetAll();

        [HttpGet("[action]/{Id}")]
        public async Task<TodoResponse<ResponseProjectGet>> Get(int Id) => await _projectBusiness.Get(Id);
    }
}