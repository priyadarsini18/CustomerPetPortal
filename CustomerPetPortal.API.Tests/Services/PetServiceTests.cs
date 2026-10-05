using CustomerPetPortal.API.DTOs.Pet;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services;
using CustomerPetPortal.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CustomerPetPortal.API.Tests.Services
{
    [TestFixture]
    public class PetServiceTests
    {
        private Mock<IPetRepository> _repositoryMock = null!;

        private Mock<ICustomerRepository>
            _customerRepositoryMock = null!;

        private Mock<IPolicyRepository>
            _policyRepositoryMock = null!;

        private PetService _service = null!;

        [SetUp]
        public void Setup()
        {
            _repositoryMock =
                new Mock<IPetRepository>();

            _customerRepositoryMock =
                new Mock<ICustomerRepository>();

            _policyRepositoryMock =
                new Mock<IPolicyRepository>();

            _service = new PetService(
                _repositoryMock.Object,
                _customerRepositoryMock.Object,
                _policyRepositoryMock.Object);
        }

        [Test]
        public void AddPet_UnknownCustomer_ShouldThrowException()
        {
            var dto = new CreatePetDto
            {
                PetName = "Tommy",
                Species = "Dog",
                Breed = "Labrador",
                DateOfBirth =
                    DateTime.Today.AddYears(-2)
            };

            _customerRepositoryMock
                .Setup(x =>
                    x.ActiveCustomerExistsAsync(1))
                .ReturnsAsync(false);

            Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await _service.AddPetAsync(
                        1,
                        dto));
        }

        [Test]
        public void AddPet_DuplicatePet_ShouldThrowException()
        {
            var dto = new CreatePetDto
            {
                PetName = "Tommy",
                Species = "Dog",
                Breed = "Labrador",
                DateOfBirth =
                    DateTime.Today.AddYears(-2)
            };

            _customerRepositoryMock
                .Setup(x =>
                    x.ActiveCustomerExistsAsync(1))
                .ReturnsAsync(true);

            _repositoryMock
                .Setup(x =>
                    x.ActivePetExistsAsync(
                        1,
                        dto.PetName))
                .ReturnsAsync(true);

            Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await _service.AddPetAsync(
                        1,
                        dto));
        }

        [Test]
        public async Task AddPet_ShouldReturnPet()
        {
            var dto = new CreatePetDto
            {
                PetName = "Tommy",
                Species = "Dog",
                Breed = "Labrador",
                DateOfBirth =
                    DateTime.Today.AddYears(-2)
            };

            _customerRepositoryMock
                .Setup(x =>
                    x.ActiveCustomerExistsAsync(1))
                .ReturnsAsync(true);

            _repositoryMock
                .Setup(x =>
                    x.ActivePetExistsAsync(
                        1,
                        dto.PetName))
                .ReturnsAsync(false);

            _repositoryMock
                .Setup(x =>
                    x.AddPetAsync(
                        It.IsAny<Pet>()))
                .ReturnsAsync(new Pet());

            var result =
                await _service.AddPetAsync(
                    1,
                    dto);

            result.Should().NotBeNull();
            result.PetName.Should().Be("Tommy");
            result.Species.Should().Be("Dog");
        }

        [Test]
        public async Task DeletePet_ShouldReturnTrue()
        {
            _policyRepositoryMock
                .Setup(x =>
                    x.HasActivePolicyForPetAsync(1))
                .ReturnsAsync(false);

            _repositoryMock
                .Setup(x =>
                    x.DeletePetAsync(1))
                .ReturnsAsync(true);

            var result =
                await _service.DeletePetAsync(1);

            result.Should().BeTrue();
        }

        [Test]
        public async Task DeletePet_ShouldReturnFalse()
        {
            _policyRepositoryMock
                .Setup(x =>
                    x.HasActivePolicyForPetAsync(999))
                .ReturnsAsync(false);

            _repositoryMock
                .Setup(x =>
                    x.DeletePetAsync(999))
                .ReturnsAsync(false);

            var result =
                await _service.DeletePetAsync(999);

            result.Should().BeFalse();
        }

        [Test]
        public async Task GetPetsByCustomerId_ShouldReturnPets()
        {
            var pets = new List<Pet>
            {
                new Pet
                {
                    PetId = 1,
                    CustomerId = 1,
                    PetName = "Tommy",
                    Species = "Dog",
                    Breed = "Labrador",
                    DateOfBirth =
                        DateTime.Today.AddYears(-2),
                    IsActive = true
                }
            };

            _repositoryMock
                .Setup(x =>
                    x.GetPetsByCustomerIdAsync(1))
                .ReturnsAsync(pets);

            var result =
                await _service
                    .GetPetsByCustomerIdAsync(1);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].PetName.Should().Be("Tommy");
        }
        [Test]
public void AddPet_FutureDob_ShouldThrowException()
{
    var dto = new CreatePetDto
    {
        PetName = "Tommy",
        Species = "Dog",
        Breed = "Labrador",
        DateOfBirth = DateTime.Today.AddDays(1)
    };

    Assert.ThrowsAsync<ArgumentException>(
        async () =>
            await _service.AddPetAsync(
                1,
                dto));
}

        [Test]
        public async Task DeletePet_WithActivePolicy_ShouldDeactivatePet()
        {
            var pet = new Pet
            {
                PetId = 1,
                CustomerId = 1,
                PetName = "Tommy",
                IsActive = true
            };

            _policyRepositoryMock
                .Setup(x =>
                    x.HasActivePolicyForPetAsync(1))
                .ReturnsAsync(true);

            _repositoryMock
                .Setup(x =>
                    x.GetPetByIdAsync(1))
                .ReturnsAsync(pet);

            var result =
                await _service.DeletePetAsync(1);

            pet.IsActive.Should().BeFalse();

            _repositoryMock.Verify(
                x => x.UpdatePetAsync(
                    It.IsAny<Pet>()),
                Times.Once);
        }
    }
}