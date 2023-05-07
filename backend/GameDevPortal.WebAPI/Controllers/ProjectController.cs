using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Extensions.EntityExtensions;
using GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;
using GameDevPortal.WebAPI.Models.Dtos.ProgressReportDtos;
using GameDevPortal.WebAPI.Models.Dtos.ProjectDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GameDevPortal.WebAPI.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private const string _getName = $"{nameof(Get)}{nameof(Project)}";
        private readonly IMapper _mapper;
        private readonly IProjectDomainService _domainService;

        public ProjectController(IMapper mapper, IProjectDomainService domainService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectGetDto>>> List([FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
        {
            var listResult = await _domainService.List(pagination, cancellationToken);

            if (!listResult.Success) throw listResult.Exception;

            IEnumerable<Project> entities = listResult.ResultData;
            IEnumerable<ProjectGetDto> dtos = _mapper.Map<IEnumerable<ProjectGetDto>>(entities);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(ProjectCreateDto createDto, CancellationToken cancellationToken = default)
        {
            Project entity = _mapper.Map<Project>(createDto);

            var saveResult = await _domainService.Insert(entity, cancellationToken);

            if (!saveResult.Success) throw saveResult.Exception;

            ProjectGetDto createdEntityDto = _mapper.Map<ProjectGetDto>(entity);

            return CreatedAtRoute(_getName, new { id = createdEntityDto.Id }, createdEntityDto);
        }

        [HttpGet("{id}", Name = _getName)]
        public async Task<ActionResult<ProjectGetDto>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Project entity = getResult.ResultData;
            ProjectGetDto dto = _mapper.Map<ProjectGetDto>(entity);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProjectUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Project entity = getResult.ResultData;
            entity.Update(updateDto);

            var updateResult = await _domainService.Update(entity, cancellationToken);

            if (!updateResult.Success && updateResult.Exception is KeyNotFoundException) return NotFound();
            if (!updateResult.Success) throw updateResult.Exception;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartial(Guid id, JsonPatchDocument<ProjectUpdateDto> patchDocument, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Project entity = getResult.ResultData;
            ProjectUpdateDto dto = _mapper.Map<ProjectUpdateDto>(entity);

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

        [HttpGet("{id}/progress-reports")]
        public async Task<ActionResult<IEnumerable<ProgressReportGetDto>>> ListProgressReports(Guid id, [FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
        {
            var listResult = await _domainService.ListProgressReports(id, pagination, cancellationToken);

            if (!listResult.Success && listResult.Exception is KeyNotFoundException) return NotFound();
            if (!listResult.Success) throw listResult.Exception;

            IEnumerable<ProgressReport> entities = listResult.ResultData;
            IEnumerable<ProgressReportGetDto> dtos = _mapper.Map<IEnumerable<ProgressReportGetDto>>(entities);

            return Ok(dtos);
        }

        [HttpGet("{id}/categories")]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> ListCategories(Guid id, [FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
        {
            var listResult = await _domainService.ListCategories(id, pagination, cancellationToken);

            if (!listResult.Success && listResult.Exception is KeyNotFoundException) return NotFound();
            if (!listResult.Success) throw listResult.Exception;

            IEnumerable<Category> entities = listResult.ResultData;
            IEnumerable<CategoryGetDto> dtos = _mapper.Map<IEnumerable<CategoryGetDto>>(entities);

            return Ok(dtos);
        }
    }
}