using DataAccess.Repositories.Abstracts;
using Models.Dtos.RequestDto;
using Moq;
using Service.BusinessRules;
using Service.Concrete;
using Models.Entities;
using Models.Dtos.ResponseDto;
using System.Net;
using Service.BusinessRules.Abstract;
using Core.CrossCuttingConcerns.Exceptions;

namespace Service.UnitTest.Products
{
    public class ProductServiceTests
    {
        //Constructor dependency injection ile nesne üretemiyoruz. Çünkü WebAPI projesindeki gibi Program.cs yok. 

        private ProductService _service;
        private Mock<IProductRepository> _mockRepository;
        private Mock<IProductRules> _mockRules;


        private ProductAddRequest productAddRequest;
        private ProductUpdateRequest productUpdateRequest;
        private Product product;
        private ProductResponseDto productResponseDto;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockRules = new Mock<IProductRules>();
            _service = new ProductService(_mockRepository.Object, _mockRules.Object);

            productAddRequest = new ProductAddRequest(Name: "Test", Stock: 25, Price: 2500, CategoryId: 1);
            productUpdateRequest = new ProductUpdateRequest(Id: new Guid(), Name: "Test", Stock: 25, Price: 2500, CategoryId: 1);
            product = new Product()
            {
                Id = new Guid(),
                Name = "Test",
                CategoryId = 1,
                Price = 2500,
                Stock = 25,
                Category = new Category() { Id = 1, Name = "Teknoloji", Products = new List<Product>() { new Product() } }
            };
            productResponseDto = new ProductResponseDto(Id: new Guid(), Name: "Test", Stock: 25, Price: 2500, CategoryId: 1);

        }
        // Yukarıda artık kulanılacak servisler hazır.

        [Test]
        public void Add_WhenProductNameIsUnique_ReturnsOk()
        {
            //Arrange (dataların hazırlandığı kısım)
            _mockRules.Setup(x => x.ProductNameMustBeUnique(productAddRequest.Name));
            _mockRepository.Setup(x => x.Add(product));
            //Yukarıda ProductService içindeki Add metodunun içindeki ConvertToEntity,ConvertToResponse metotları alınmadı.

            //Act (servisi çalıştırdığımız yer)
            var result = _service.Add(productAddRequest);

            //Assert (yukarıda Act ile belirtilen metot çalıştırıldıktan sonra karşılaşabileceğimiz sonuçları test ettiğimiz yer aşağıda)
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual(result.Data, productResponseDto);
            Assert.AreEqual(result.Message, "Ürün Eklendi");



        }

        [Test]
        public void Add_WhenProductNameIsUnique_ReturnsBadRequest()
        {
            //Arrange
            _mockRules.Setup(x => x.ProductNameMustBeUnique(productAddRequest.Name)).Throws(new BusinessException("Ürün ismi benzersiz olmalı"));
            //_service.Add metodu çalıştıktan sonra ProductNameMustBeUnique satırında exception fırlatacağından dolayı yukarıdaki ilk metottaki gibi _mockRepository.Setup(x => x.Add(product)); satırı kullanılmadı. 

            //Act
            var result = _service.Add(productAddRequest);

            //Assert
            Assert.AreEqual(result.Message, "Ürün ismi benzersiz olmalı");
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

    }
}
