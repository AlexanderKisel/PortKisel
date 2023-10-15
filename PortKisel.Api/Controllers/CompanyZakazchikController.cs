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
    public class CompanyZakazchikController : ControllerBase
    {
        private readonly ICompanyZakazchikService companyZakazchikService;

        public CompanyZakazchikController(ICompanyZakazchikService companyZakazchikService)
        {
            this.companyZakazchikService = companyZakazchikService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await companyZakazchikService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new CompanyZakazchikResponse
            {
                Id = x.Id,
                CompanyZakazchikName = x.CompanyZakazchikName,
                CompanyZakazchikDescription = x.CompanyZakazchikDescription,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await companyZakazchikService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new CompanyZakazchikResponse
            {
                Id = result.Id,
                CompanyZakazchikName = result.CompanyZakazchikName,
                CompanyZakazchikDescription = result.CompanyZakazchikDescription,
            });
        }
    }
}
