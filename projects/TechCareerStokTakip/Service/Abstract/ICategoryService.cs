using Core.Shared;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
namespace Service.Abstract;

public interface ICategoryService
{
    Response<CategoryResponseDto> Add(CategoryAddRequest categoryAddRequest);
    Response<CategoryResponseDto> Update(CategoryUpdateRequest categoryUpdateRequest);
    Response<CategoryResponseDto> Delete(int id);
    Response<CategoryResponseDto> GetById(int id);
    Response<List<CategoryResponseDto>> GetAll();
}
