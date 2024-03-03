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
    public class CategoriesController : BaseController
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
            return ActionResultInstance(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryUpdateRequest categoryUpdateRequest)
        {
            Response<CategoryResponseDto> result = _categoryService.Update(categoryUpdateRequest);
            return ActionResultInstance(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            Response<CategoryResponseDto> result = _categoryService.Delete(id);
            return ActionResultInstance(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] int id)
        {
            Response<CategoryResponseDto> result = _categoryService.GetById(id);
            return ActionResultInstance(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Response<List<CategoryResponseDto>> result = _categoryService.GetAll();
            return ActionResultInstance(result);
        }
    }
}
