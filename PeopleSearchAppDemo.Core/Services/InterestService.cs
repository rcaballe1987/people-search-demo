using Microsoft.EntityFrameworkCore;
using PeopleSearchAppDemo.Core.Domain;
using PeopleSearchAppDemo.Core.Models;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Core.Services
{
    public class InterestService : IInterestService
    {
        private readonly PeopleSearchAppDemoDbContext _context;

        public InterestService(PeopleSearchAppDemoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <see cref="IInterestService.Get"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<InterestModel>> Get()
        {
            return await _context.Interests
                .Select(interest => new InterestModel()
                {
                    Id = interest.Id,
                    Name = interest.Name
                })
                .ToListAsync();
        }
    }
}
