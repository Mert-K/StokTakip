using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Service.BusinessRules.Abstract;

namespace Service.BusinessRules;
//aşağıdaki IProductRules interface'i UnitTest için oluşturuldu. UnitTest içinde(ProductServiceTests.cs) ProductRules için instantiate hatası almamak için.
public class ProductRules : IProductRules
{
    private readonly IProductRepository _productRepository;

    public ProductRules(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void ProductNameMustBeUnique(string productName)
    {
        var product = _productRepository.GetByFilter(p => p.Name == productName);
        if (product is not null)
        {
            throw new BusinessException("Ürün ismi benzersiz olmalı");
        }
    }

    public void ProductIsPresent(Guid id)
    {
        var product = _productRepository.GetById(id);
        if (product is null)
        {
            throw new BusinessException($"Id si : {id} olan ürün bulunamadı.");
        }
    }
}
