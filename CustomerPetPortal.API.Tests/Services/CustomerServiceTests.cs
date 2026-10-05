using CustomerPetPortal.API.DTOs.Customer;
using CustomerPetPortal.API.Exceptions;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services;
using CustomerPetPortal.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CustomerPetPortal.API.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository>
            _repositoryMock = null!;

        private Mock<ILogger<CustomerService>>
            _loggerMock = null!;

        private Mock<IPolicyRepository>
            _policyRepositoryMock = null!;

        private Mock<IAddressRepository>
            _addressRepositoryMock = null!;

        private CustomerService _service = null!;

        [SetUp]
        public void Setup()
        {
            _repositoryMock =
                new Mock<ICustomerRepository>();

            _loggerMock =
                new Mock<ILogger<CustomerService>>();

            _policyRepositoryMock =
                new Mock<IPolicyRepository>();

            _addressRepositoryMock =
                new Mock<IAddressRepository>();

            _service = new CustomerService(
                _repositoryMock.Object,
                _loggerMock.Object,
                _policyRepositoryMock.Object,
                _addressRepositoryMock.Object);
        }

        [Test]
        public async Task CreateCustomer_ShouldReturnCustomer()
        {
            var dto = new CreateCustomerDto
            {
                FirstName = "Priya",
                LastName = "S",
                Email = "priya@gmail.com",
                Phone = "9876543210",
                CommunicationPreference = "Email",
                AddressLine1 = "ABC Street",
                City = "Chennai",
                State = "Tamil Nadu",
                ZipCode = "600001",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            _repositoryMock
                .Setup(x => x.EmailExistsAsync(dto.Email))
                .ReturnsAsync(false);

            _repositoryMock
                .Setup(x => x.AddCustomerAsync(
                    It.IsAny<Customer>()))
                .ReturnsAsync(new Customer());

            _addressRepositoryMock
                .Setup(x => x.AddAddressAsync(
                    It.IsAny<Address>()))
                .ReturnsAsync(new Address());

            var result =
                await _service.CreateCustomerAsync(dto);

            result.Should().NotBeNull();
            result.Email.Should().Be(dto.Email);
            result.FirstName.Should().Be(dto.FirstName);
        }

        [Test]
        public void CreateCustomer_PasswordMismatch_ShouldThrowException()
        {
            var dto = new CreateCustomerDto
            {
                FirstName = "Priya",
                LastName = "S",
                Email = "priya@gmail.com",
                Password = "Password123",
                ConfirmPassword = "Password456"
            };

            Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await _service.CreateCustomerAsync(dto));
        }

        [Test]
        public void CreateCustomer_DuplicateEmail_ShouldThrowException()
        {
            var dto = new CreateCustomerDto
            {
                Email = "priya@gmail.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            _repositoryMock
                .Setup(x => x.EmailExistsAsync(dto.Email))
                .ReturnsAsync(true);

            Assert.ThrowsAsync<DuplicateEmailException>(
                async () =>
                    await _service.CreateCustomerAsync(dto));
        }

        [Test]
        public async Task GetCustomerById_ShouldReturnCustomer()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Priya",
                LastName = "S",
                Email = "priya@gmail.com",
                Phone = "9876543210",
                CommunicationPreference = "Email"
            };

            _repositoryMock
                .Setup(x => x.GetCustomerByIdAsync(1))
                .ReturnsAsync(customer);

            var result =
                await _service.GetCustomerByIdAsync(1);

            result.Should().NotBeNull();
            result!.CustomerId.Should().Be(1);
            result.Email.Should().Be(customer.Email);
        }

        [Test]
        public async Task GetCustomerById_ShouldReturnNull_WhenCustomerNotFound()
        {
            _repositoryMock
                .Setup(x => x.GetCustomerByIdAsync(999))
                .ReturnsAsync((Customer?)null);

            var result =
                await _service.GetCustomerByIdAsync(999);

            result.Should().BeNull();
        }

        [Test]
        public async Task DeleteCustomer_ShouldReturnTrue()
        {
            _policyRepositoryMock
                .Setup(x =>
                    x.GetPoliciesByCustomerIdAsync(1))
                .ReturnsAsync(
                    new List<CustomerPolicy>());

            _repositoryMock
                .Setup(x => x.DeleteCustomerAsync(1))
                .ReturnsAsync(true);

            var result =
                await _service.DeleteCustomerAsync(1);

            result.Should().BeTrue();
        }

        [Test]
        public async Task DeleteCustomer_ShouldReturnFalse()
        {
            _policyRepositoryMock
                .Setup(x =>
                    x.GetPoliciesByCustomerIdAsync(999))
                .ReturnsAsync(
                    new List<CustomerPolicy>());

            _repositoryMock
                .Setup(x => x.DeleteCustomerAsync(999))
                .ReturnsAsync(false);

            var result =
                await _service.DeleteCustomerAsync(999);

            result.Should().BeFalse();
        }

        [Test]
        public void DeleteCustomer_WithActivePolicy_ShouldThrowException()
        {
            _policyRepositoryMock
                .Setup(x =>
                    x.GetPoliciesByCustomerIdAsync(1))
                .ReturnsAsync(
                    new List<CustomerPolicy>
                    {
                        new CustomerPolicy
                        {
                            Status = "Active"
                        }
                    });

            Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await _service.DeleteCustomerAsync(1));
        }
    }
}