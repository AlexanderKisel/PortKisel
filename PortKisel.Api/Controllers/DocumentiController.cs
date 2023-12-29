using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models;
using PortKisel.Services.Contracts.Interface;
using System.ComponentModel.DataAnnotations;

namespace PortKisel.Controllers
{
    /// <summary>
    /// CRUD контрлллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Documenti")]
    public class DocumentiController : ControllerBase
    {
        private readonly IDocumentiService documentiService;
        private readonly IMapper mapper;

        public DocumentiController(IDocumentiService documentiService, IMapper mapper)
        {
            this.documentiService = documentiService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DocumentiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await documentiService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DocumentiResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DocumentiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await documentiService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<DocumentiResponse>(result));
        }
    }
}
