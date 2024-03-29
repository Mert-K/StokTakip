﻿using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using Service.BusinessRules;
using System.Net;

namespace Service.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryRules _categoryRules;

    public CategoryService(ICategoryRepository categoryRepository, CategoryRules categoryRules)
    {
        _categoryRepository = categoryRepository;
        _categoryRules = categoryRules;
    }

    public Response<CategoryResponseDto> Add(CategoryAddRequest categoryAddRequest)
    {
        try
        {
            Category category = categoryAddRequest;
            _categoryRules.CategoryNameMustBeUnique(category.Name);
            _categoryRepository.Add(category);

            CategoryResponseDto categoryResponseDto = category;
            return new Response<CategoryResponseDto>
            {
                Data = categoryResponseDto,
                Message = "Kategori Eklendi",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDto>()
            {
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CategoryResponseDto> Delete(int id)
    {
        try
        {
            _categoryRules.CategoryIsPresent(id);
            Category? category = _categoryRepository.GetById(id);
            _categoryRepository.Delete(category);

            CategoryResponseDto categoryResponseDto = category;
            return new Response<CategoryResponseDto>
            {
                Data = categoryResponseDto,
                Message = "Kategori Silindi",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDto>()
            {
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }



    }

    public Response<List<CategoryResponseDto>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> response = categories.Select(c => new CategoryResponseDto(Id: c.Id, Name: c.Name)).ToList();
        return new Response<List<CategoryResponseDto>>
        {
            Data = response,
            StatusCode = HttpStatusCode.OK
        };
    }

    public Response<CategoryResponseDto> GetById(int id)
    {
        try
        {
            _categoryRules.CategoryIsPresent(id);
            Category? category = _categoryRepository.GetById(id);
            CategoryResponseDto response = category;

            return new Response<CategoryResponseDto>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDto>
            {
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CategoryResponseDto> Update(CategoryUpdateRequest categoryUpdateRequest)
    {
        try
        {
            Category category = categoryUpdateRequest;
            _categoryRules.CategoryNameMustBeUnique(category.Name);
            _categoryRepository.Update(category);

            CategoryResponseDto response = category;
            return new Response<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori Güncellendi",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDto>()
            {
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}
