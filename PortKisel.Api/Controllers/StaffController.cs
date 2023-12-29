using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models;
using PortKisel.Services.Contracts.Interface;

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

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            this.staffService = staffService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<StaffResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await staffService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<StaffResponse>(result));
        }
    }
}