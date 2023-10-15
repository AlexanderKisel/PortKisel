using Microsoft.AspNetCore.Mvc;
using PortKisel.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с компаниями перевозчиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyPerController : ControllerBase
    {
        private readonly ICompanyPerService companyPerService;

        public CompanyPerController(ICompanyPerService companyPerService)
        {
            this.companyPerService = companyPerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await companyPerService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new CompanyPerResponse
            {
                Id = x.Id,
                CompanyPerName = x.CompanyPerName,
                CompanyPerDescription = x.CompanyPerDescription,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyPerService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new CompanyPerResponse
            {
                Id = result.Id,
                CompanyPerName = result.CompanyPerName,
                CompanyPerDescription = result.CompanyPerDescription,
            });
        }
    }
}
