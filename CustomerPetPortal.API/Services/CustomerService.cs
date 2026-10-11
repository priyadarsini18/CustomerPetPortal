using CustomerPetPortal.API.DTOs.Customer;
using CustomerPetPortal.API.Exceptions;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;
using Microsoft.Extensions.Logging;

namespace CustomerPetPortal.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        private readonly ILogger<CustomerService>
            _logger;

        private readonly IPolicyRepository
            _policyRepository;

        private readonly IAddressRepository
            _addressRepository;

        public CustomerService(
            ICustomerRepository repository,
            ILogger<CustomerService> logger,
            IPolicyRepository policyRepository,
            IAddressRepository addressRepository)
        {
            _repository = repository;
            _logger = logger;
            _policyRepository = policyRepository;
            _addressRepository = addressRepository;
        }

        public async Task<CustomerDto> CreateCustomerAsync(
            CreateCustomerDto dto)
        {
            _logger.LogInformation(
                "Creating customer with email {Email}",
                dto.Email);

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new InvalidOperationException(
                    "Password and Confirm Password do not match.");
            }

            bool emailExists =
                await _repository
                    .EmailExistsAsync(dto.Email);

            if (emailExists)
            {
                _logger.LogWarning(
                    "Duplicate email attempted: {Email}",
                    dto.Email);

                throw new DuplicateEmailException();
            }

            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                CommunicationPreference =
                    dto.CommunicationPreference,
                IsActive = true
            };

            await _repository
                .AddCustomerAsync(customer);

            var address = new Address
            {
                CustomerId = customer.CustomerId,
                Line1 = dto.AddressLine1,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode
            };

            await _addressRepository
                .AddAddressAsync(address);

            _logger.LogInformation(
                "Customer created successfully. CustomerId: {CustomerId}",
                customer.CustomerId);

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                CommunicationPreference =
                    customer.CommunicationPreference
            };
        }

        public async Task<List<CustomerDto>>
            GetAllCustomersAsync()
        {
            _logger.LogInformation(
                "Fetching all customers");

            var customers =
                await _repository
                    .GetAllCustomersAsync();

            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                CommunicationPreference =
                    c.CommunicationPreference
            }).ToList();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(
            int id)
        {
            _logger.LogInformation(
                "Fetching customer with ID {CustomerId}",
                id);

            var customer =
                await _repository
                    .GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning(
                    "Customer not found. CustomerId: {CustomerId}",
                    id);

                return null;
            }

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                CommunicationPreference =
                    customer.CommunicationPreference
            };
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(
            int id,
            UpdateCustomerDto dto)
        {
            _logger.LogInformation(
                "Updating customer with ID {CustomerId}",
                id);

            var customer =
                await _repository
                    .GetCustomerByIdAsync(id);

            if (customer == null)
            {
                _logger.LogWarning(
                    "Customer not found for update. CustomerId: {CustomerId}",
                    id);

                return null;
            }

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.CommunicationPreference =
                dto.CommunicationPreference;

            await _repository
                .UpdateCustomerAsync(customer);

            _logger.LogInformation(
                "Customer updated successfully. CustomerId: {CustomerId}",
                id);

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                CommunicationPreference =
                    customer.CommunicationPreference
            };
        }

        public async Task<bool> DeleteCustomerAsync(
            int id)
        {
            _logger.LogInformation(
                "Deleting customer with ID {CustomerId}",
                id);

            var policies =
                await _policyRepository
                    .GetPoliciesByCustomerIdAsync(id);

            // LINQ All Demonstration
            bool canDelete =
                policies.All(
                    p => p.Status != "Active");

            if (!canDelete)
            {
                throw new InvalidOperationException(
                    "Customer has active policies and cannot be deleted.");
            }

            var deleted =
                await _repository
                    .DeleteCustomerAsync(id);

            if (deleted)
            {
                _logger.LogInformation(
                    "Customer deleted successfully. CustomerId: {CustomerId}",
                    id);
            }
            else
            {
                _logger.LogWarning(
                    "Customer not found for deletion. CustomerId: {CustomerId}",
                    id);
            }

            return deleted;
        }
        public async Task<CustomerDto?> LoginAsync(
    string email)
{
    var customer =
        await _repository
            .GetCustomerByEmailAsync(email);

    if (customer == null)
    {
        return null;
    }

    return new CustomerDto
    {
        CustomerId = customer.CustomerId,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        Email = customer.Email,
        Phone = customer.Phone,
        CommunicationPreference =
            customer.CommunicationPreference
    };
}
   
    }
}