using AutoMapper;
using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Models;
using GameDevPortal.WebAPI.Extensions;
using GameDevPortal.WebAPI.Extensions.EntityExtensions;
using GameDevPortal.WebAPI.Models.Dtos.CategoryDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GameDevPortal.WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private const string _getName = $"{nameof(Get)}{nameof(Category)}";
        private readonly IMapper _mapper;
        private readonly ICategoryDomainService _domainService;

        public CategoryController(IMapper mapper, ICategoryDomainService domainService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> List([FromQuery][Optional] Pagination pagination, CancellationToken cancellationToken = default)
        {
            var listResult = await _domainService.List(pagination, cancellationToken);

            if (!listResult.Success) throw listResult.Exception;

            IEnumerable<Category> entities = listResult.ResultData;
            IEnumerable<CategoryGetDto> dtos = _mapper.Map<IEnumerable<CategoryGetDto>>(entities);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CategoryCreateDto createDto, CancellationToken cancellationToken = default)
        {
            Category entity = _mapper.Map<Category>(createDto);

            var saveResult = await _domainService.Insert(entity, cancellationToken);

            if (!saveResult.Success) throw saveResult.Exception;

            CategoryGetDto createdEntityDto = _mapper.Map<CategoryGetDto>(entity);

            return CreatedAtRoute(_getName, new { id = createdEntityDto.Id }, createdEntityDto);
        }

        [HttpGet("{id}", Name = _getName)]
        public async Task<ActionResult<CategoryGetDto>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Category entity = getResult.ResultData;
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(entity);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CategoryUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Category entity = getResult.ResultData;
            entity.Update(updateDto);

            var updateResult = await _domainService.Update(entity, cancellationToken);

            if (!updateResult.Success && updateResult.Exception is KeyNotFoundException) return NotFound();
            if (!updateResult.Success) throw updateResult.Exception;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartial(Guid id, JsonPatchDocument<CategoryUpdateDto> patchDocument, CancellationToken cancellationToken = default)
        {
            var getResult = await _domainService.Get(id, cancellationToken);

            if (!getResult.Success && getResult.Exception is KeyNotFoundException) return NotFound();
            if (!getResult.Success) throw getResult.Exception;

            Category entity = getResult.ResultData;
            CategoryUpdateDto dto = _mapper.Map<CategoryUpdateDto>(entity);

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