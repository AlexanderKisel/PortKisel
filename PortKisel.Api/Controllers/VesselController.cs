using Microsoft.AspNetCore.Mvc;
using PortKisel.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с суднами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class VesselController : ControllerBase
    {
        private readonly IVesselService vesselService;

        public VesselController(IVesselService vesselService)
        {
            this.vesselService = vesselService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await vesselService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new VesselResponse
            {
                Id = x.Id,
                NameVessel = x.NameVessel,
                Description = x.Description,
                CompanyPerId = x.CompanyPerId,
                LoadCapacity = x.LoadCapacity,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await vesselService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new VesselResponse
            {
                Id = result.Id,
                NameVessel = result.NameVessel,
                Description = result.Description,
                CompanyPerId = result.CompanyPerId,
                LoadCapacity = result.LoadCapacity,
            });
        }
    }
}
