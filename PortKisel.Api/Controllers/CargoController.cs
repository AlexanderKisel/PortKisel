using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с компаниями заказчиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Cargo")]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService cargoService;
        private readonly IMapper mapper;

        public CargoController(ICargoService cargoService, IMapper mapper)
        {
            this.cargoService = cargoService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CargoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cargoService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CargoResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await cargoService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти груз с идентификатором {id}");
            }

            return Ok(mapper.Map<CargoResponse>(result));
        }
    }
}
