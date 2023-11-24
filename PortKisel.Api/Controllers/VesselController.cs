using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models;
using PortKisel.Services.Contracts.Interface;

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

        public VesselController(IVesselService vesselService, IMapper mapper)
        {
            this.vesselService = vesselService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(VesselResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await vesselService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<VesselResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(VesselResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await vesselService.GetByAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<VesselResponse>(result));
        }
    }
}
