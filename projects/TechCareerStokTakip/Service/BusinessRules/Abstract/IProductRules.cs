namespace Service.BusinessRules.Abstract;

public interface IProductRules
{
    void ProductNameMustBeUnique(string productName);
    void ProductIsPresent(Guid id);
}
