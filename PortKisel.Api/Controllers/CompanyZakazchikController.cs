using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;
using PortKisel.Services.Implementations;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с компаниями заказчиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "CompanyZakazchik")]
    public class CompanyZakazchikController : ControllerBase
    {
        private readonly ICompanyZakazchikService companyZakazchikService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;

        public CompanyZakazchikController(ICompanyZakazchikService companyZakazchikService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.companyZakazchikService = companyZakazchikService;
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
            var result = await companyZakazchikService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CompanyZakazchikResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyZakazchikService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<CompanyZakazchikResponse>(result));
        }

        /// <summary>
        /// Создаёт новую компанию заказчика
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateCompanyZakazhikRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var companyZakazchikRequestModel = mapper.Map<CompanyZakazchikRequestModel>(request);
            var result = await companyZakazchikService.AddAsync(companyZakazchikRequestModel, cancellationToken);
            return Ok(mapper.Map<CompanyPerResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся компанию заказчика
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditCompanyZakazhikRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<CompanyZakazchikRequestModel>(request);
            var result = await companyZakazchikService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<CompanyZakazchikResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся компанию заказчика по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await companyZakazchikService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
