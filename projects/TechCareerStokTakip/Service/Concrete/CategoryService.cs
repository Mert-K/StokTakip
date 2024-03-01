using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using System.Net;

namespace Service.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Response<CategoryResponseDto> Add(CategoryAddRequest categoryAddRequest)
    {
        Category category = categoryAddRequest;
        _categoryRepository.Add(category);

        CategoryResponseDto categoryResponseDto = category;
        return new Response<CategoryResponseDto>
        {
            Data = categoryResponseDto,
            Message = "Kategori Eklendi",
            StatusCode = HttpStatusCode.Created
        };

    }

    public Response<CategoryResponseDto> Delete(int id)
    {
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
        Category? category = _categoryRepository.GetById(id);
        CategoryResponseDto response = category;

        return new Response<CategoryResponseDto>
        {
            Data = response,
            StatusCode = HttpStatusCode.OK
        };
    }

    public Response<CategoryResponseDto> Update(CategoryUpdateRequest categoryUpdateRequest)
    {
        Category category = categoryUpdateRequest;
        _categoryRepository.Update(category);

        CategoryResponseDto response = category;
        return new Response<CategoryResponseDto>
        {
            Data = response,
            Message = "Kategori Güncellendi",
            StatusCode = HttpStatusCode.OK
        };

    }
}
