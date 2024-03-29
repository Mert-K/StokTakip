﻿using Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
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
            return ActionResultInstance(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductUpdateRequest productUpdateRequest)
        {
            Response<ProductResponseDto> result = _productService.Update(productUpdateRequest);
            return ActionResultInstance(result);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            Response<ProductResponseDto> result = _productService.Delete(id);
            return ActionResultInstance(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            Response<ProductResponseDto> result = _productService.GetById(id);
            return ActionResultInstance(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Response<List<ProductResponseDto>> result = _productService.GetAll();
            return ActionResultInstance(result);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetAllByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            Response<List<ProductResponseDto>> result = _productService.GetAllByPriceRange(min, max);
            return ActionResultInstance(result);
        }

        [HttpGet("getbydetailid")]
        public IActionResult GetByDetailId([FromQuery] Guid id)
        {
            var result = _productService.GetByDetailId(id);
            return ActionResultInstance(result);

        }

        [HttpGet("details")]
        public IActionResult GetAllDetails()
        {
            var result = _productService.GetAllDetails();
            return ActionResultInstance(result);
        }

        [HttpGet("getalldetailsbycategory")]
        public IActionResult GetAllDetailsByCategoryId([FromQuery] int categoryId)
        {
            var result = _productService.GetAllDetailsByCategoryId(categoryId);
            return ActionResultInstance(result);
        }
    }
}
