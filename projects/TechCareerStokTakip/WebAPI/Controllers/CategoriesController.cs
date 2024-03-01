using Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CategoryAddRequest categoryAddRequest)
        {
            Response<CategoryResponseDto> result = _categoryService.Add(categoryAddRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Created("/", result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryUpdateRequest categoryUpdateRequest)
        {
            Response<CategoryResponseDto> result = _categoryService.Update(categoryUpdateRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            Response<CategoryResponseDto> result = _categoryService.Delete(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] int id)
        {
            Response<CategoryResponseDto> result = _categoryService.GetById(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Response<List<CategoryResponseDto>> result = _categoryService.GetAll();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
