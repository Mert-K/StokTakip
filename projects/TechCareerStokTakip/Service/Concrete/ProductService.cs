using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using Service.BusinessRules;
using Service.BusinessRules.Abstract;

namespace Service.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductRules _rules;

    public ProductService(IProductRepository productRepository, IProductRules rules)
    {
        _productRepository = productRepository;
        _rules = rules;
    }

    public Response<ProductResponseDto> Add(ProductAddRequest request)
    {
        try
        {
            Product product = ProductAddRequest.ConvertToEntity(request);
            _rules.ProductNameMustBeUnique(product.Name);

            product.Id = new Guid();
            _productRepository.Add(product);
            var data = ProductResponseDto.ConvertToResponse(product);
            return new Response<ProductResponseDto>()
            {
                Data = data,
                Message = "Ürün Eklendi",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ProductResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ProductResponseDto> Delete(Guid id)
    {
        try
        {
            _rules.ProductIsPresent(id);
            Product? product = _productRepository.GetById(id);
            _productRepository.Delete(product);
            var data = ProductResponseDto.ConvertToResponse(product);
            return new Response<ProductResponseDto>()
            {
                Data = data,
                Message = "Ürün Silindi",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ProductResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<ProductResponseDto>> GetAll()
    {
        List<Product> products = _productRepository.GetAll();
        List<ProductResponseDto> response = products.Select(p => ProductResponseDto.ConvertToResponse(p)).ToList();

        return new Response<List<ProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ProductResponseDto>> GetAllByPriceRange(decimal min, decimal max)
    {
        List<Product> products = _productRepository.GetAll(p => p.Price <= max && p.Price >= min);

        List<ProductResponseDto> response = products.Select(p => ProductResponseDto.ConvertToResponse(p)).ToList();
        return new Response<List<ProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };

    }

    public Response<List<ProductDetailDto>> GetAllDetails()
    {
        List<ProductDetailDto> details = _productRepository.GetAllProductDetails();
        return new Response<List<ProductDetailDto>>()
        {
            Data = details,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ProductDetailDto>> GetAllDetailsByCategoryId(int categoryId)
    {
        List<ProductDetailDto> details = _productRepository.GetDetailsByCategoryId(categoryId);
        return new Response<List<ProductDetailDto>>()
        {
            Data = details,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ProductDetailDto> GetByDetailId(Guid id)
    {
        try
        {
            _rules.ProductIsPresent(id);
            ProductDetailDto? productDetail = _productRepository.GetProductDetail(id);
            return new Response<ProductDetailDto>()
            {
                Data = productDetail,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ProductDetailDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ProductResponseDto> GetById(Guid id)
    {
        try
        {
            _rules.ProductIsPresent(id);
            Product? product = _productRepository.GetById(id);
            ProductResponseDto productResponseDto = ProductResponseDto.ConvertToResponse(product);
            return new Response<ProductResponseDto>()
            {
                Data = productResponseDto,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ProductResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }


    }

    public Response<ProductResponseDto> Update(ProductUpdateRequest request)
    {
        try
        {
            Product product = ProductUpdateRequest.ConvertToEntity(request);
            _rules.ProductNameMustBeUnique(product.Name);
            _productRepository.Update(product);

            ProductResponseDto response = ProductResponseDto.ConvertToResponse(product);
            return new Response<ProductResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ProductResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
