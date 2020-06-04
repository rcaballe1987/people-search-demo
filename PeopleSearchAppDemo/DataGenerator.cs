using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PeopleSearchAppDemo.Core.Domain;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PeopleSearchAppDemo.Api
{
    public class DataGenerator
    {
        public static void InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new PeopleSearchAppDemoDbContext(serviceProvider.GetRequiredService<DbContextOptions<PeopleSearchAppDemoDbContext>>()))
            {
                var fileService = serviceProvider.GetRequiredService<IFileService>();

                /** Seed Interst **/
                if (context.Interests.Any())
                {
                    return;   // Data was already seeded
                }

                var interestId1 = Guid.NewGuid();
                var interestId2 = Guid.NewGuid();
                var interestId3 = Guid.NewGuid();

                context.Interests.AddRange(
                    new Interest()
                    { 
                        Id = interestId1,
                        Name = "Music"
                    },
                    new Interest()
                    {
                        Id = interestId2,
                        Name = "Sports"
                    },
                    new Interest()
                    {
                        Id = interestId3,
                        Name = "Travel"
                    });

                /** Seed Person **/
                if (context.Persons.Any())
                {
                    return;   // Data was already seeded
                }

                var personId1 = Guid.Parse("59c2dee5-f4ee-454c-a5d8-703895ba0729");
                var personId2 = Guid.Parse("e6b808de-2f71-4b31-9dba-0d36a451fc5f");
                var personId3 = Guid.Parse("cb3ffe21-ccf1-4cda-92c6-c22686429874");

                context.Persons.AddRange(
                    new Person()
                    {
                        Id = personId1,
                        FirstName = "John",
                        LastName = "Walker",
                        Address = "225 Liberty Square, New City, OH 22541",
                        Age = 25,
                        ImageLink = fileService.UploadImage("59c2dee5-f4ee-454c-a5d8-703895ba0729.jpg"),
                        PersonInterests = new Collection<PersonInterest>()
                        { 
                            new PersonInterest()
                            {
                                Id = Guid.NewGuid(),
                                PersonId = personId1,
                                InterestId = interestId1
                            }
                        }
                    },
                    new Person()
                    {
                        Id = personId2,
                        FirstName = "Jane",
                        LastName = "Brown",
                        Address = "12547 Comanche Ave, Tampa, FL 33631",
                        Age = 78,
                        ImageLink = fileService.UploadImage("e6b808de-2f71-4b31-9dba-0d36a451fc5f.png"),
                        PersonInterests = new Collection<PersonInterest>()
                        {
                            new PersonInterest()
                            {
                                Id = Guid.NewGuid(),
                                PersonId = personId2,
                                InterestId = interestId2
                            }
                        }
                    },
                    new Person()
                    {
                        Id = personId3,
                        FirstName = "Peter",
                        LastName = "Young",
                        Address = "1245 Game St, Louisville, KY 11987",
                        Age = 18,
                        ImageLink = fileService.UploadImage("cb3ffe21-ccf1-4cda-92c6-c22686429874.png"),
                        PersonInterests = new Collection<PersonInterest>()
                        {
                            new PersonInterest()
                            {
                                Id = Guid.NewGuid(),
                                PersonId = personId3,
                                InterestId = interestId3
                            }
                        }
                    });

                context.SaveChanges();
            }
        }
    }
}
