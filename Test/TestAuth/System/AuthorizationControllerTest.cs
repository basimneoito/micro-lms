using Authorization.Interfaces;
using Authorization.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.TestAuth.Mock;

namespace Test.TestAuth;

public class AuthorizationControllerTest
{
    
    [Fact]
    public async Task LoginAsync_ShouldReturn200Status()
    {
       /// Arrange
       var authService = new Mock<IAuthService>();
       authService.Setup(_ => _.LoginAsync(AuthorizationMockData.GetLoginRequest())).ReturnsAsync(AuthorizationMockData.GetLoginResponse);
       var sut = new AuthorizationController(authService.Object);
       ///Assign
       var result = (OkObjectResult) await sut.LoginAsync(AuthorizationMockData.GetLoginRequest());
       ///Assert
       result.StatusCode.Should().Be(200);

    }

    [Fact]
    public async Task RegisterAsync_ShouldReturn200Status()
    {
       /// Arrange
       var authService = new Mock<IAuthService>();
       authService.Setup(_ => _.RegisterAsync(AuthorizationMockData.GetRegisterRequest())).ReturnsAsync(AuthorizationMockData.GetRegisterResponse());
       var sut = new AuthorizationController(authService.Object);
       ///Assign
       var result = (OkObjectResult) await sut.RegisterAsync(AuthorizationMockData.GetRegisterRequest());
       ///Assert
       result.StatusCode.Should().Be(200);

    }
}