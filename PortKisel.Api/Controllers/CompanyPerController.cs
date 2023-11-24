using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models;
using PortKisel.Services.Contracts.Interface;

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

        public CompanyPerController(ICompanyPerService companyPerService, IMapper mapper)
        {
            this.companyPerService = companyPerService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyPerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await companyPerService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CompanyPerResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CompanyPerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyPerService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<CompanyPerResponse>(result));
        }
    }
}
