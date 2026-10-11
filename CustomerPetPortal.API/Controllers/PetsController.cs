using CustomerPetPortal.API.DTOs.Pet;
using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetPortal.API.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _service;

        public PetsController(IPetService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPet(
            int customerId,
            CreatePetDto dto)
        {
            if (dto.DateOfBirth.Date > DateTime.Today)
            {
                return BadRequest(
                    "Pet Date of Birth cannot be in the future."
                );
            }

            var pet =
                await _service.AddPetAsync(
                    customerId,
                    dto
                );

            return Ok(pet);
        }

        [HttpGet]
        public async Task<IActionResult> GetPets(
            int customerId)
        {
            var pets =
                await _service.GetPetsByCustomerIdAsync(
                    customerId
                );

            return Ok(pets);
        }

        [HttpPut("{petId}")]
        public async Task<IActionResult> UpdatePet(
            int petId,
            UpdatePetDto dto)
        {
            if (dto.DateOfBirth.Date > DateTime.Today)
            {
                return BadRequest(
                    "Pet Date of Birth cannot be in the future."
                );
            }

            var pet =
                await _service.UpdatePetAsync(
                    petId,
                    dto
                );

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpDelete("{petId}")]
        public async Task<IActionResult> DeletePet(
            int petId)
        {
            var result =
                await _service.DeletePetAsync(
                    petId
                );

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}