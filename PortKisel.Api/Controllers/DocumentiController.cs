using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Attribute;
using PortKisel.Api.Infrastructures.Validator;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Documenti;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;
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
        private readonly IApiValidatorService validatorService;

        public DocumentiController(IDocumentiService documentiService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.documentiService = documentiService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        [HttpGet]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await documentiService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DocumentiResponse>>(result));
        }

        [HttpGet("{id:guid}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await documentiService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<DocumentiResponse>(result));
        }
        /// <summary>
        /// Создаёт новый документ
        /// </summary>
        [HttpPost]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Create(CreateDocumentiRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var documentiRequestModel = mapper.Map<DocumentiRequestModel>(request);
            var result = await documentiService.AddAsync(documentiRequestModel, cancellationToken);
            return Ok(mapper.Map<DocumentiResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся документ
        /// </summary>
        [HttpPut]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Edit(EditDocumentiRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<DocumentiRequestModel>(request);
            var result = await documentiService.UpdateAsync(model, cancellationToken);
            return Ok(mapper.Map<DocumentiResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся документ по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiConflict]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await documentiService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
