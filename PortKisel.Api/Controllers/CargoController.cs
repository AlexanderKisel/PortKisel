using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;

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
        private readonly IApiValidatorService validatorService;

        public CargoController(ICargoService cargoService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.cargoService = cargoService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        [HttpGet]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cargoService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CargoResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await cargoService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти груз с идентификатором {id}");
            }

            return Ok(mapper.Map<CargoResponse>(result));
        }

        /// <summary>
        /// Создаёт новый груз
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateCargoRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var cargoRequestModel = mapper.Map<CargoRequestModel>(request);
            var result = await cargoService.AddAsync(cargoRequestModel, cancellationToken);
            return Ok(mapper.Map<CargoResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся груз
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditCargoRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<CargoRequestModel>(request);
            var result = await cargoService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<CargoResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся груз по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await cargoService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
