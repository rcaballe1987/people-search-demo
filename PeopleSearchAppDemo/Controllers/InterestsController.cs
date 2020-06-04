using Microsoft.AspNetCore.Mvc;
using PeopleSearchAppDemo.Core.Models;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestsController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        /// <summary>
        /// Get list of <see cref="InterestModel"> models
        /// </summary>
        /// <returns>List of <see cref="InterestModel"> models</returns>
        [HttpGet]
        public async Task<List<InterestModel>> Get()
        {
            return await _interestService.Get();
        }
    }
}
