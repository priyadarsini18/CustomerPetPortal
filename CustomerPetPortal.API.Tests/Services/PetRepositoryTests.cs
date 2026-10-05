using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories;
using CustomerPetPortal.Models;

using FluentAssertions;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace CustomerPetPortal.API.Tests.Repositories
{
    [TestFixture]
    public class PetRepositoryTests
    {
        private SqliteConnection _connection = null!;
        private AppDbContext _context = null!;
        private PetRepository _repository = null!;

        [SetUp]
        public async Task Setup()
        {
            _connection =
                new SqliteConnection(
                    "DataSource=:memory:");

            await _connection.OpenAsync();

            var options =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(_connection)
                    .Options;

            _context =
                new AppDbContext(options);

            await _context.Database.EnsureCreatedAsync();

            _repository =
                new PetRepository(_context);

            await SeedCustomerAsync();
        }

        private async Task SeedCustomerAsync()
        {
            if (!await _context.Customers.AnyAsync())
            {
                _context.Customers.Add(
                    new Customer
                    {
                        CustomerId = 1,
                        FirstName = "Priyadarsini",
                        LastName = "S",
                        Email = "priyadarsini@gmail.com",
                        Phone = "9876543210",
                        CommunicationPreference = "Email"
                    });

                await _context.SaveChangesAsync();
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.DisposeAsync();
            await _connection.DisposeAsync();
        }

        [Test]
        public async Task AddPet_ShouldInsertPet()
        {
            var pet = new Pet
            {
                CustomerId = 1,
                PetName = "Tommy",
                Species = "Dog",
                Breed = "Labrador",
                DateOfBirth =
                    DateTime.Today.AddYears(-2),

                IsActive = true
            };

            await _repository.AddPetAsync(pet);

            var result =
                await _context.Pets
                    .FirstOrDefaultAsync(
                        p => p.PetName == "Tommy");

            result.Should().NotBeNull();

            result!.PetName
                .Should()
                .Be("Tommy");
        }

        [Test]
        public async Task GetPetById_ShouldReturnPet()
        {
            var pet = new Pet
            {
                CustomerId = 1,
                PetName = "Rocky",
                Species = "Dog",
                Breed = "Beagle",
                DateOfBirth =
                    DateTime.Today.AddYears(-1),

                IsActive = true
            };

            _context.Pets.Add(pet);

            await _context.SaveChangesAsync();

            var result =
                await _repository
                    .GetPetByIdAsync(pet.PetId);

            result.Should().NotBeNull();

            result!.PetName
                .Should()
                .Be("Rocky");
        }

        [Test]
        public async Task GetPetsByCustomerId_ShouldReturnPets()
        {
            _context.Pets.AddRange(

                new Pet
                {
                    CustomerId = 1,
                    PetName = "Tommy",
                    Species = "Dog",
                    Breed = "Labrador",
                    DateOfBirth =
                        DateTime.Today.AddYears(-2),

                    IsActive = true
                },

                new Pet
                {
                    CustomerId = 1,
                    PetName = "Rocky",
                    Species = "Dog",
                    Breed = "Beagle",
                    DateOfBirth =
                        DateTime.Today.AddYears(-1),

                    IsActive = true
                });

            await _context.SaveChangesAsync();

            var result =
                await _repository
                    .GetPetsByCustomerIdAsync(1);

            result.Should()
                .HaveCount(2);
        }
    }
}