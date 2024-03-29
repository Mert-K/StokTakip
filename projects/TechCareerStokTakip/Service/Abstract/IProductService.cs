﻿using Core.Shared;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;

namespace Service.Abstract;

public interface IProductService
{
    Response<ProductResponseDto> Add(ProductAddRequest request);
    Response<ProductResponseDto> Update(ProductUpdateRequest request);
    Response<ProductResponseDto> Delete(Guid id);
    Response<ProductResponseDto> GetById(Guid id);
    Response<List<ProductResponseDto>> GetAll();
    Response<List<ProductResponseDto>> GetAllByPriceRange(decimal min, decimal max);
    Response<ProductDetailDto> GetByDetailId(Guid id);
    Response<List<ProductDetailDto>> GetAllDetails();
    Response<List<ProductDetailDto>> GetAllDetailsByCategoryId(int categoryId);
}
