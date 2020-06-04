using Microsoft.AspNetCore.Mvc;
using PeopleSearchAppDemo.Core.Models;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Get list of <see cref="PersonModel"> models
        /// </summary>
        /// <returns>List of <see cref="PersonModel"> models</returns>
        [HttpGet]
        public async Task<List<PersonModel>> Get()
        {
            return await _personService.Get();
        }

        /// <summary>
        /// Get a <see cref="PersonModel"> model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="PersonModel"> model</returns>
        [HttpGet("{id:guid}")]
        public async Task<PersonModel> Get([FromRoute] Guid id)
        {
            return await _personService.Get(id);
        }

        /// <summary>
        /// Adds a Person
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns>Created <see cref="PersonModel"> model</returns>
        [HttpPost]
        public async Task<PersonModel> Post([FromBody] PersonModel personModel)
        {
            return await _personService.Add(personModel);
        }

        /// <summary>
        /// Updates a Person
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns>Updated <see cref="PersonModel"> model</returns>
        [HttpPut]
        public async Task<PersonModel> Put([FromBody] PersonModel personModel)
        {
            return await _personService.Update(personModel);
        }

        /// <summary>
        /// Deletes a Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean value indicating whether the Person was successfully deleted</returns>
        [HttpDelete("{id:guid}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _personService.Delete(id);
        }
    }
}
