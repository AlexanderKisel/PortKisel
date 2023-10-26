using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PortKisel.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocumentiController : ControllerBase
    {
        private readonly IDocumentiService documentiService;

        public DocumentiController(IDocumentiService documentiService)
        {
            this.documentiService = documentiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await documentiService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DocumentiResponse
            {
                Id = x.Id,
                NumberDoc = x.NumberDoc,
                IssaedAt = x.IssaedAt,
                CargoId = x.CargoId,
                VesselId = x.VesselId,
                CompanyPerId = x.CompanyPerId,
                CompanyZakazchikId = x.CompanyZakazchikId,
                Posts = x.Posts.GetDisplayName(),
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await documentiService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new DocumentiResponse
            {
                Id = result.Id,
                NumberDoc = result.NumberDoc,
                IssaedAt = result.IssaedAt,
                CargoId = result.CargoId,
                VesselId = result.VesselId,
                CompanyPerId = result.CompanyPerId,
                CompanyZakazchikId = result.CompanyZakazchikId,
                Posts = result.Posts.GetDisplayName(),
            });
        }
    }
}
