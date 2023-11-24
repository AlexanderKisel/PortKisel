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
    [ApiExplorerSettings(GroupName = "CompanyZakazchik")]
    public class CompanyZakazchikController : ControllerBase
    {
        private readonly ICompanyZakazchikService companyZakazchikService;
        private readonly IMapper mapper;

        public CompanyZakazchikController(ICompanyZakazchikService companyZakazchikService, IMapper mapper)
        {
            this.companyZakazchikService = companyZakazchikService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CompanyZakazchikResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await companyZakazchikService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<CompanyZakazchikResponse>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CompanyZakazchikResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyZakazchikService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<CompanyZakazchikResponse>(result));
        }
    }
}
