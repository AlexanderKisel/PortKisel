using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Vessel;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;
using PortKisel.Services.Implementations;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с суднами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Vessel")]
    public class VesselController : ControllerBase
    {
        private readonly IVesselService vesselService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;

        public VesselController(IVesselService vesselService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.vesselService = vesselService;
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
            var result = await vesselService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<VesselResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await vesselService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<VesselResponse>(result));
        }

        /// <summary>
        /// Создаёт новое судно
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateVesselRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var vesselRequestModel = mapper.Map<VesselRequestModel>(request);
            var result = await vesselService.AddAsync(vesselRequestModel, cancellationToken);
            return Ok(mapper.Map<VesselResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся судно
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditVesselRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<VesselRequestModel>(request);
            var result = await vesselService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<VesselResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся судно по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await vesselService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
