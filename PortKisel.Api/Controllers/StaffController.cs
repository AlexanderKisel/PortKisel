using Microsoft.AspNetCore.Mvc;
using PortKisel.Models;
using PortKisel.Services.Contracts.Interface;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;

        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new StaffResponse
            {
                Id = x.Id,
                FIO = x.FIO,
                Posts = x.Posts,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await staffService.GetByAsync(id, cancellationToken);
            if(result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(new StaffResponse
            {
                Id = result.Id,
                FIO = result.FIO,    
                Posts = result.Posts,
            });
        }
    }
}