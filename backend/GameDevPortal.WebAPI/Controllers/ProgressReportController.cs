using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Extensions;
using GameDevPortal.WebAPI.Extensions.EntityExtensions;
using GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;
using GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GameDevPortal.WebAPI.Controllers
{
    [ApiController]
    [Route("api/progress-reports")]
    public class ProgressReportController : ControllerBase
    {
        private const string _getName = $"{nameof(Get)}{nameof(ProgressReport)}";
        private readonly IMapper _mapper;
        private readonly IProgressReportDomainService _domainService;

        public ProgressReportController(IMapper mapper, IProgressReportDomainService domainService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgressReportGetDto>>> List([FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
        {
            var listResult = await _domainService.List(pagination, cancellationToken);

            if (!listResult.Success) throw listResult.Exception;

            IEnumerable<ProgressReport> entities = listResult.ResultData;
            IEnumerable<ProgressReportGetDto> dtos = _mapper.Map<IEnumerable<ProgressReportGetDto>>(entities);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(ProgressReportCreateDto createDto, CancellationToken cancellationToken = default)
        {
            ProgressReport entity = _mapper.Map<ProgressReport>(createDto);

            var saveResult = await _domainService.Insert(entity, cancellationToken);

            if (!saveResult.Success && saveResult.Exception is KeyNotFoundException) return NotFound(saveResult.Exception.Message);
            else if (!saveResult.Success) throw saveResult.Exception;

            ProgressReportGetDto createdEntityDto = _mapper.Map<ProgressReportGetDto>(entity);

            return CreatedAtRoute(_getName, new { id = createdEntityDto.Id }, createdEntityDto);
        }

        [HttpGet("{id}", Name = _getName)]
        public async Task<ActionResult<ProgressReportGetDto>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            ProgressReport entity = getResult.ResultData;
            ProgressReportGetDto dto = _mapper.Map<ProgressReportGetDto>(entity);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProgressReportUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            ProgressReport entity = getResult.ResultData;
            entity.Update(updateDto);

            var updateResult = await _domainService.Update(entity, cancellationToken);

            if (!updateResult.Success && updateResult.Exception is KeyNotFoundException) return NotFound();
            if (!updateResult.Success) throw updateResult.Exception;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartial(Guid id, JsonPatchDocument<ProgressReportUpdateDto> patchDocument, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            ProgressReport entity = getResult.ResultData;
            ProgressReportUpdateDto dto = _mapper.Map<ProgressReportUpdateDto>(entity);

            patchDocument.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TryValidateModel(ModelState)) return BadRequest(ModelState);

            entity.Update(dto);

            var updateResult = await _domainService.Update(entity, cancellationToken);

            if (!updateResult.Success) throw updateResult.Exception;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteResult = await _domainService.Delete(id, cancellationToken);

            if (!deleteResult.Success && deleteResult.Exception is KeyNotFoundException) return NotFound();
            if (!deleteResult.Success) throw deleteResult.Exception;

            return NoContent();
        }
    }
}