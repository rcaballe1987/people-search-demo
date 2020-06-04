using PeopleSearchAppDemo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Core.ServiceContracts
{
    public interface IInterestService
    {
        /// <summary>
        /// Gets a list of <see cref="InterestModel">
        /// </summary>
        /// <returns>List of <see cref="InterestModel"> model</returns>
        Task<List<InterestModel>> Get();
    }
}
