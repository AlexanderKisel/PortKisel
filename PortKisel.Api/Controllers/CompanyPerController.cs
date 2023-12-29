using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;
using PortKisel.Services.Implementations;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с компаниями перевозчиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "CompanyPer")]
    public class CompanyPerController : ControllerBase
    {
        private readonly ICompanyPerService companyPerService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;

        public CompanyPerController(ICompanyPerService companyPerService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.companyPerService = companyPerService;
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
            var result = await companyPerService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CompanyPerResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyPerService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<CompanyPerResponse>(result));
        }

        /// <summary>
        /// Создаёт новую компанию перевозчика
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateCompanyPerRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var companyPerRequestModel = mapper.Map<CompanyPerRequestModel>(request);
            var result = await companyPerService.AddAsync(companyPerRequestModel, cancellationToken);
            return Ok(mapper.Map<CompanyPerResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся компанию перевозчика
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditCompanyPerRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken); 

            var model = mapper.Map<CompanyPerRequestModel>(request);
            var result = await companyPerService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<CompanyPerResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся компанию перевозчика по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await companyPerService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
