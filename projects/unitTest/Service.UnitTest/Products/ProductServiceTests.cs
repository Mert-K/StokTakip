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
using Core.Shared;
using NuGet.Frameworks;
using System.Xml.XPath;

namespace Service.UnitTest.Products
{
    public class ProductServiceTests
    {
        //Constructor dependency injection ile nesne üretemiyoruz. Çünkü WebAPI projesindeki gibi Program.cs yok. 

        private ProductService _service;
        private Mock<IProductRepository> _mockRepository;
        private Mock<IProductRules> _mockRules;

        //bunlar sonradan eklendi, önce yukarıdakiler eklendi.
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

        [Test]
        public void Delete_WhenProductIsPresent_ReturnsOk()
        {
            Guid id = new Guid();
            //Arrange
            _mockRules.Setup(x => x.ProductIsPresent(id));
            _mockRepository.Setup(x => x.GetById(id, null)).Returns(product);
            _mockRepository.Setup(x => x.Delete(product));

            //Act
            var result = _service.Delete(id);

            //Assert
            Assert.AreEqual(result.Data, productResponseDto);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(result.Message, "Ürün Silindi");
        }

        [Test]
        public void Delete_WhenProductIsNotPresent_ReturnsBadRequest()
        {
            //Arrange
            Guid id = new Guid();
            _mockRules.Setup(x => x.ProductIsPresent(id)).Throws(new BusinessException($"Id si : {id} olan ürün bulunamadı."));

            //Act
            var result = _service.Delete(id);

            //Assert
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
            Assert.AreEqual(result.Message, $"Id si : {id} olan ürün bulunamadı.");
        }

        [Test]
        public void GetAll_ReturnOk()
        {

            var products = new List<Product>()
            {
                product
            };

            var responses = new List<ProductResponseDto>()
            {
                productResponseDto
            };

            //Arrange
            _mockRepository.Setup(x => x.GetAll(null, null)).Returns(products); //IProductRepository'nin yani 

            //Action
            var result = _service.GetAll(); //ProductService'in GetAll() metodu çalıştırıldığında ilk satırda ProductRepository'nin GetAll() metodu çalıştırılacak ve List<Product> dönecek. Bu dönecek olan List<Product>'ı yukarıda Arrange ile belirttik. Ondan sonra servisteki GetAll metodu devam ediyor ve yukarıdaki products değişkeninde bulunan product'lara Select işlemi uygulayıp List<ProductResponseDto> dönüyor. Ondan sonra devam edip; return new Response<List<ProductResponseDto>>() dönüyor. Bu dönen return new Response<List<ProductResponseDto>>() değerleri ile Aşağıda Assert ile belirttiğimiz değerler uyuşuyorsa testten geçiyor (ki aynı değerleri biz veriyoruz, yukarıda oluşturduğumuz değişkenler ile).

            //Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(result.Data, responses);
        }

        [Test]
        public void GetByDetailId_WhenDetailIsPresent_ReturnsOk()
        {
            ProductDetailDto dto = new ProductDetailDto()
            {
                CategoryName = "Test",
                Id = new Guid(),
                Name = "Test",
                Price = 1000,
                Stock = 25
            };

            //Arrange
            _mockRules.Setup(x => x.ProductIsPresent(It.IsAny<Guid>())); //herhangi bir Guid vermek istemiyorsak bu şekilde de yazabiliriz. ProductIsPresent metodu void olduğu için aşağıdaki gibi .Returns yazmaya gerek yok. Bu satırı yorum satırına alsak dahi testten geçer.
            _mockRepository.Setup(x => x.GetProductDetail(It.IsAny<Guid>())).Returns(dto); //bu metodu çalıştırdıktan sonra bana yukarıdaki dto nesnesini dön.

            //Action
            var result = _service.GetByDetailId(It.IsAny<Guid>());

            //Assert
            Assert.AreEqual(result.Data, dto);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void GetByDetailId_WhenDetailIsNotPresent_ReturnsBadRequest()
        {
            //Arrange
            _mockRules.Setup(x => x.ProductIsPresent(It.IsAny<Guid>())).Throws(new BusinessException($"Id si : {It.IsAny<Guid>()} olan ürün bulunamadı."));

            //Act
            var result = _service.GetByDetailId(It.IsAny<Guid>());

            //Assert
            Assert.AreEqual(result.Message, $"Id si : {It.IsAny<Guid>()} olan ürün bulunamadı.");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
