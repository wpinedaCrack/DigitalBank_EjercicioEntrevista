using APIRest.Controllers;
using APIRest.Domain.IServices;
using Castle.Core.Configuration;
using Moq;
using NUnit.Framework;

namespace ApiRest_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mockService = new Mock<ILoginService>();
            var mockConfig = new Mock<IConfiguration>();

            LoginController login = new LoginController(mockService.Object, mockConfig.Object);
            //LoginController login = new LoginController(mockService.Object, mockConfig.Object);
            Assert.Pass();
        }
    }
}