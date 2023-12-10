using Business;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ToDo_Planning.Controllers.Developer
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperBusiness _developerBusiness;

        public DeveloperController(IDeveloperBusiness developerBusiness)
        {
            _developerBusiness = developerBusiness;
        }

        [HttpGet("[action]/{Id}")]
        public async Task<TodoResponse<ResponseDeveloperGet>> Get(int Id) => await _developerBusiness.Get(Id);
    }
}