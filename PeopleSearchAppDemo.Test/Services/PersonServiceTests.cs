using ClearlyAgile.TimeTracker.Tests;
using NSubstitute;
using PeopleSearchAppDemo.Core.Domain;
using PeopleSearchAppDemo.Core.Models;
using PeopleSearchAppDemo.Core.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PeopleSearchAppDemo.Test.Services
{
    public class PersonServiceTests : TestSpec<PersonService>
    {
        [Fact]
        public async Task Add_Should_ThrowArgumentNullException_When_PersonModelNull()
        {
            // Arrange
            var sut = Sut();

            // Act - Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Add(null));
            exception.Message.ShouldContain("No PersonModel passed to Add function.");
        }

        [Fact]
        public async Task Add_Should_SavePerson_When_ValidationPasses()
        {
            // Arrange
            PersonModel personModel = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Address = "Fake Address",
                Age = 19,
                PersonInterestIds = new List<Guid>()
            };

            var sut = Sut();

            // Act
            var newPerson = await sut.Add(personModel);

            // Assert
            await Mock<PeopleSearchAppDemoDbContext>().Received(1).SaveChangesAsync();
        }

        [Theory]
        [InlineData("Jane", "Watson", "Address 1", 47)]
        [InlineData("Julia", "Roberts", "Address 2", 39)]
        [InlineData("Peter", "Hook", "Address 3", 21)]
        [InlineData("Amanda", "Nielsen", "Address 4", 65)]
        public async Task Add_Should_ReturnTheNewPerson_When_ValidationPasses(string firstName, string lastName, string address, int age)
        {
            // Arrange
            PersonModel personModel = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Age = age,
                PersonInterestIds = new List<Guid>()
            };

            var sut = Sut();

            // Act
            var newPerson = await sut.Add(personModel);

            // Assert
            newPerson.ShouldNotBeNull();
            newPerson.ShouldBeOfType(typeof(PersonModel));
            newPerson.Id.ShouldNotBe(personModel.Id);
            newPerson.FirstName.ShouldBe(personModel.FirstName);
            newPerson.LastName.ShouldBe(personModel.LastName);
            newPerson.Age.ShouldBe(personModel.Age);
        }

        [Fact]
        public async Task Delete_Should_ThrowArgumentNullException_When_IdNull()
        {
            // Arrange
            var id = default(Guid);
            var sut = Sut();

            // Act - Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Delete(id));
            exception.Message.ShouldContain("Value does not exist.");
        }
    }
}
