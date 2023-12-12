using Business;
using Microsoft.AspNetCore.Mvc;
using Model;
using ToDo_Planning.Business;
using ToDo_Planning.Model;

namespace ToDo_Planning.Controllers.Provider
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleBusiness _ScheduleBusiness;

        public ScheduleController(IScheduleBusiness ScheduleBusiness)
        {
            _ScheduleBusiness = ScheduleBusiness;
        }

        [HttpGet("[action]/{Id}")]
        public async Task<TodoResponse<ResponseScheduleGet>> Get(int Id) => await _ScheduleBusiness.Get(Id);
    }
}