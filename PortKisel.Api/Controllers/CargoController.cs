using Microsoft.AspNetCore.Mvc;
using PortKisel.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с компаниями заказчиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService cargoService;

        public CargoController(ICargoService cargoService)
        {
            this.cargoService = cargoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cargoService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new CargoResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Weight = x.Weight,
                CompanyZakazchikId = x.CompanyZakazchikId,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await cargoService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new CargoResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Weight = result.Weight,
                CompanyZakazchikId = result.CompanyZakazchikId,
            });
        }
    }
}
