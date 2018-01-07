using Xunit;
using IdentityTask.Controllers;
using IdentityTask.Models;
using IdentityTask.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityTask.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async void RegisterOkResultMethodTest()
        {
            var mock = new Mock<IUserService>();
            var model = new RegisterModel();
            mock.Setup(s => s.Register(model)).ReturnsAsync(IdentityResult.Success);

            var controller = new AccountController(mock.Object);
            var result =await controller.Register(model) as OkResult;

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void RegisterBadRequestMethodTest()
        {
            var model = new RegisterModel();

            var mock = new Mock<IUserService>();
            mock.Setup(s => s.Register(model)).ReturnsAsync(IdentityResult.Failed());

            var controller = new AccountController(mock.Object);
            var result = await controller.Register(model) is OkResult;

            Assert.False(result);
        }

        [Fact]
        public async void TokenActionOkRequestMethod()
        {
            var model = new LoginModel()
            {
                Email = "test@gmail.com"
            };

            var mock = new Mock<IUserService>();
            mock.Setup(s => s.LoginToken(model)).ReturnsAsync("okJW Tok");            

            var controller = new AccountController(mock.Object);
            if (!(await controller.Token(model) is OkObjectResult res)) return;
            if (res.StatusCode.HasValue)
            {
                var is200 = res.StatusCode.Value == 200;
                Assert.True(is200);
            }
        }

        [Fact]
        public async void TokenActionBadRequestMethod()
        {
            var model = new LoginModel()
            {
                Email = "test@gmail.com"
            };

            var mock = new Mock<IUserService>();
            mock.Setup(s => s.LoginToken(model)).ReturnsAsync("okJW Tok");

            var controller = new AccountController(mock.Object);
            var res = await controller.Token(model) is OkObjectResult;
 
            Assert.False(res);
        }
    }
}
