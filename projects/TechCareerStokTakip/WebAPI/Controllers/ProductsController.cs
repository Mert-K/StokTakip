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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductAddRequest productAddRequest)
        {
            Response<ProductResponseDto> result = _productService.Add(productAddRequest);

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Created("/", result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductUpdateRequest productUpdateRequest)
        {
            Response<ProductResponseDto> result = _productService.Update(productUpdateRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            Response<ProductResponseDto> result = _productService.Delete(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            Response<ProductResponseDto> result = _productService.GetById(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Response<List<ProductResponseDto>> result = _productService.GetAll();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetAllByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            Response<List<ProductResponseDto>> result = _productService.GetAllByPriceRange(min, max);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbydetailid")]
        public IActionResult GetByDetailId([FromQuery] Guid id)
        {
            var result = _productService.GetByDetailId(id);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("details")]
        public IActionResult GetAllDetails()
        {
            var result = _productService.GetAllDetails();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetailsbycategory")]
        public IActionResult GetAllDetailsByCategoryId([FromQuery] int categoryId)
        {
            var result = _productService.GetAllDetailsByCategoryId(categoryId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
