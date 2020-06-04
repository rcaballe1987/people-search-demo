using Microsoft.EntityFrameworkCore;
using PeopleSearchAppDemo.Core.Domain;
using PeopleSearchAppDemo.Core.Models;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearchAppDemo.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly PeopleSearchAppDemoDbContext _context;

        public PersonService(PeopleSearchAppDemoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <see cref="IPersonService.Add(PersonModel)"/>
        /// </summary>
        /// <param name="personModel"></param>
        public async Task<PersonModel> Add(PersonModel personModel)
        {
            if (personModel == null)
            {
                throw new ArgumentNullException("No PersonModel passed to Add function.");
            }

            Person person = GetEntity(personModel);
            person.Id = Guid.NewGuid();

            ICollection<PersonInterest> interests = new Collection<PersonInterest>();
            if (personModel.PersonInterestIds.Count > 0)
            {
                foreach (var interestId in personModel.PersonInterestIds)
                {
                    PersonInterest personInterest = new PersonInterest()
                    {
                        Id = Guid.NewGuid(),
                        PersonId = person.Id,
                        InterestId = interestId
                    };
                    interests.Add(personInterest);
                }
            }

            await _context.AddRangeAsync(interests);
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();

            return GetModel(person);
        }

        /// <summary>
        /// <see cref="IPersonService.Delete(Guid)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            if (id == null || id == default(Guid))
            {
                throw new ArgumentNullException(nameof(id), "Value does not exist.");
            }

            Person person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                throw new ValidationException("Person does not exist.");
            }

            try
            {
                _context.Remove(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// <see cref="IPersonService.Get"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonModel>> Get()
        {
            return await _context.Persons
                .Select(person => new PersonModel()
                { 
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Address = person.Address,
                    Age = person.Age,
                    ImageLink = person.ImageLink,
                    PersonInterestIds = person.PersonInterests.Select(x => x.InterestId).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// <see cref="IPersonService.Get(Guid)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PersonModel> Get(Guid id)
        {
            if (id == null || id == default(Guid))
            {
                throw new ArgumentNullException(nameof(id), "Value does not exist.");
            }

            Person person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                throw new ValidationException("Person does not exist.");
            }

            return GetModel(person);
        }

        /// <summary>
        /// <see cref="IPersonService.Update(PersonModel)"/>
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        public Task<PersonModel> Update(PersonModel personModel)
        {
            throw new NotImplementedException();
        }

        private Person GetEntity(PersonModel personModel)
        {
            return new Person()
            {
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                Address = personModel.Address,
                Age = personModel.Age,
            };
        }

        private PersonModel GetModel(Person person)
        {
            return new PersonModel()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address = person.LastName,
                Age = person.Age,
                ImageLink = person.ImageLink,
                PersonInterestIds = person.PersonInterests.Select(x => x.Id).ToList()
            };
        }
    }
}
