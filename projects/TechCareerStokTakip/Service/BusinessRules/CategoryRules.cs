using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;

namespace Service.BusinessRules;

public class CategoryRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void CategoryNameMustBeUnique(string categoryName)
    {
        var category = _categoryRepository.GetByFilter(c => c.Name == categoryName);
        if (category != null)
        {
            throw new BusinessException("Kategori adı benzersiz olmalı");
        }
    }

    public void CategoryIsPresent(int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category == null)
        {
            throw new BusinessException($"Id : {id} olan kategori bulunamadı");
        }
    }
}
