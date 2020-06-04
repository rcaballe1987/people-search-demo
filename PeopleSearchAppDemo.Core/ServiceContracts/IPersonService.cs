using PeopleSearchAppDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Core.ServiceContracts
{
    public interface IPersonService
    {
        /// <summary>
        /// Gets a list of <see cref="PersonModel">
        /// </summary>
        /// <returns>List of <see cref="PersonModel"> model</returns>
        Task<List<PersonModel>> Get();

        /// <summary>
        /// Get a <see cref="PersonModel"> model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="PersonModel"> model</returns>
        Task<PersonModel> Get(Guid id);

        /// <summary>
        /// Adds a new Person
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns>Added <see cref="PersonModel"> model</returns>
        Task<PersonModel> Add(PersonModel personModel);

        /// <summary>
        /// Updates a Person
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns>Updated <see cref="PersonModel"> model</returns>
        Task<PersonModel> Update(PersonModel personModel);

        /// <summary>
        /// Deletes a Person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean value indicating whether the action was successful or not</returns>
        Task<bool> Delete(Guid id);
    }
}
