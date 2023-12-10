using Business;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ToDo_Planning.Controllers.Provider
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerBusiness;

        public ProviderController(IProviderService providerBusiness)
        {
            _providerBusiness = providerBusiness;
        }

        [HttpPost("[action]")]
        public async Task<TodoResponse<ResponseSaveProject>> Save(RequestProjectModel request) => await _providerBusiness.SaveAsync(request);
    }
}