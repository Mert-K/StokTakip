using Core.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.ResponseDto;
using WebAPI.Controllers;
namespace WebAPI.UnitTest;

public class BaseControllerTests
{
    private BaseController _controller;

    [SetUp]
    public void SetUp()
    {
        _controller = new BaseController();
    }

    [Test]
    public void ActionResultInstance_WhenStatusCodeOk_ReturnsOk()
    {
        //Arrange
        var response = new Response<string>()
        {
            Data = "Test",
            Message = "Test",
            StatusCode = System.Net.HttpStatusCode.OK
        };

        //Act
        var result = _controller.ActionResultInstance(response);

        //Asset
        Assert.IsInstanceOf<OkObjectResult>(result); //result'ın veri tipinin OkObjectResult tipinde olup olmadığına bakar. ActionResultInstance metodu return Ok() metodu döndürdüğü için ve Ok() metodu OkObjectResult nesnesi oluşturduğu için bu şekilde karşılaştırma yapılır.
    }


    [Test]
    public void ActionResultInstance_WhenStatusCodeNotFound_ReturnsNotFound()
    {
        //Arrange
        var response = new Response<string>()
        {
            Data = "Test",
            Message = "Test",
            StatusCode = System.Net.HttpStatusCode.NotFound
        };
        //Act
        var result = _controller.ActionResultInstance(response);

        //Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public void ActionResultInstance_WhenStatusCodeBadRequest_ReturnsBadRequest()
    {
        //Arrange
        var response = new Response<string>()
        {
            Data = "Test",
            Message = "Test",
            StatusCode = System.Net.HttpStatusCode.BadRequest
        };

        //Act
        var result = _controller.ActionResultInstance(response);

        //Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public void ActionResultInstance_WhenStatusCodeCreated_ReturnsCreated()
    {
        //Arrange
        var response = new Response<string>()
        {
            Data = "Test",
            Message = "Test",
            StatusCode = System.Net.HttpStatusCode.Created
        };

        //Act
        var result = _controller.ActionResultInstance(response);

        //Assert
        Assert.IsInstanceOf<CreatedResult>(result);
    }


}
