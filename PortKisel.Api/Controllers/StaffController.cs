using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Staff;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;
using System.ComponentModel.DataAnnotations;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Staff")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;

        public StaffController(IStaffService staffService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.staffService = staffService;
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
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<StaffResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById([Required]Guid id, CancellationToken cancellationToken)
        {
            var result = await staffService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<StaffResponse>(result));
        }
        /// <summary>
        /// Создаёт нового работника
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateStaffRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var staffRequestModel = mapper.Map<StaffRequestModel>(request);
            var result = await staffService.AddAsync(staffRequestModel, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющегося работника
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditStaffRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<StaffRequestModel>(request);
            var result = await staffService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющегося работника по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await staffService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}