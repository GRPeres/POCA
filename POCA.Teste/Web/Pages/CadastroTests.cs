using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using POCA.Web.Pages.User;
using POCA.Web.Services;
using POCA.Web.Requests.Pessoa;
using POCA.Web.Requests.Aluno;
using RichardSzalay.MockHttp;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using POCA.Web.Services.APIs;

namespace POCA.Teste.Web.Pages
{
    [TestFixture]
    public class CadastroTests
    {
        private Bunit.TestContext _ctx;
        private Mock<UserSessionService> _mockUserSessionService;
        private Mock<PessoasAPI> _mockPessoasAPI;
        private Mock<AlunosAPI> _mockAlunosAPI;
        private Mock<IConfiguration> _mockConfiguration;

        [SetUp]
        public void Setup()
        {
            _ctx = new Bunit.TestContext();
            _mockUserSessionService = new Mock<UserSessionService>();
            _mockPessoasAPI = new Mock<PessoasAPI>(new HttpClient());
            _mockAlunosAPI = new Mock<AlunosAPI>(new HttpClient());
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
            _ctx.Services.AddSingleton(_mockAlunosAPI.Object);
        }

        [TearDown] public void TearDown() => _ctx.Dispose();

        [Test]
        public void Cadastro_RendersCorrectly()
        {
            // Arrange
            _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(false);

            // Act
            var cut = _ctx.RenderComponent<Cadastro>();

            // Assert
            Assert.IsTrue(cut.Markup.Contains("Cadastro"));
        }

        [Test]
        public void Cadastro_WithMismatchedPasswords_ShowsWarning()
        {
            // Arrange
            _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(false);
            var cut = _ctx.RenderComponent<Cadastro>();

            cut.Find("input[label=\"Login\"]").Change("testuser");
            cut.Find("input[label=\"Nome\"]").Change("Test User");
            cut.Find("input[label=\"Email\"]").Change("test@example.com");
            cut.Find("input[label=\"Contato\"]").Change("123456789");
            cut.Find("input[label=\"Senha\"]").Change("password");
            cut.Find("input[label=\"Confirme sua Senha\"]").Change("differentpassword");

            // Act
            cut.Find("button").Click();

            // Assert
            Assert.IsTrue(cut.Markup.Contains("As senhas não são iguais"));
        }
    }
}
