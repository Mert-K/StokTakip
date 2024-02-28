using Models.Entities;

namespace Models.Dtos.ResponseDto;

public record CategoryResponseDto(int Id, string Name)
{
    public static implicit operator CategoryResponseDto(Category category)
    {
        return new CategoryResponseDto(Id: category.Id, Name: category.Name);
    }

    //CategoryResponseDto response = category;
}
