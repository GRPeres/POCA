
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using POCA.Web.Pages.User;
using POCA.Web.Services;
using POCA.Web.Requests.Pessoa;
using RichardSzalay.MockHttp;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace POCA.Teste.Web.Pages
{
    [TestFixture]
    public class LoginTests
    {
        private Bunit.TestContext _ctx;
        private Mock<UserSessionService> _mockUserSessionService;
        private Mock<PessoasAPI> _mockPessoasAPI;
        private Mock<IConfiguration> _mockConfiguration;

        [SetUp]
        public void Setup()
        {
            _ctx = new Bunit.TestContext();
            _mockUserSessionService = new Mock<UserSessionService>();
            _mockPessoasAPI = new Mock<PessoasAPI>(new HttpClient());
            _mockConfiguration = new Mock<IConfiguration>();

            var inMemorySettings = new Dictionary<string, string> {
                {"Security:Pepper", "test-pepper"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _ctx.Services.AddSingleton(configuration);
            _ctx.Services.AddSingleton(_mockUserSessionService.Object);
            _ctx.Services.AddSingleton(_mockPessoasAPI.Object);
        }

        [TearDown]
        public void TearDown() => _ctx.Dispose();

        [Test]
        public void Login_RendersCorrectly()
        {
            // Arrange
            _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(false);

            // Act
            var cut = _ctx.RenderComponent<Login>();

            // Assert
            Assert.IsTrue(cut.Markup.Contains("Login"));
        }

        [Test]
        public void Login_WithValidCredentials_LogsIn()
        {
            // Arrange
            _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(false);
            _mockPessoasAPI.Setup(api => api.LoginAsync(It.IsAny<PessoaLoginRequest>()))
                .ReturnsAsync(new PessoaAuthResponse(1, "test", false, 1, null, "token"));

            var cut = _ctx.RenderComponent<Login>();
            cut.Find("input[type='text']").Change("testuser");
            cut.Find("input[type='password']").Change("password");

            // Act
            cut.Find("button").Click();

            // Assert
            _mockPessoasAPI.Verify(api => api.LoginAsync(It.IsAny<PessoaLoginRequest>()), Times.Once);
        }
    }
}
