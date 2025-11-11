
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using POCA.Web.Pages.User;
using POCA.Web.Services;
using POCA.Web.Services.APIs;
using POCA.Web.Response;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using POCA.Web.Response.Login;

namespace POCA.Teste.Web.Pages
{
    [TestFixture]
    public class PerfilTests
    {
        private Bunit.TestContext _ctx;
        private Mock<UserSessionService> _mockUserSessionService;
        private Mock<PessoasAPI> _mockPessoasAPI;
        private Mock<AlunosAPI> _mockAlunosAPI;
        private Mock<ProfessoresAPI> _mockProfessoresAPI;
        private Mock<NavigationManager> _mockNavigationManager;

        [SetUp]
        public void Setup()
        {
            _ctx = new Bunit.TestContext();
            _mockUserSessionService = new Mock<UserSessionService>();
            _mockPessoasAPI = new Mock<PessoasAPI>(new HttpClient());
            _mockAlunosAPI = new Mock<AlunosAPI>(new HttpClient());
            _mockProfessoresAPI = new Mock<ProfessoresAPI>(new HttpClient());
            _mockNavigationManager = new Mock<NavigationManager>();

            _ctx.Services.AddSingleton(_mockUserSessionService.Object);
            _ctx.Services.AddSingleton(_mockPessoasAPI.Object);
            _ctx.Services.AddSingleton(_mockAlunosAPI.Object);
            _ctx.Services.AddSingleton(_mockProfessoresAPI.Object);
            _ctx.Services.AddSingleton(_mockNavigationManager.Object);
        }

        [TearDown]
        public void TearDown() => _ctx.Dispose();

        [Test]
        public void Perfil_NotLoggedIn_ShowsLoginAndRegisterButtons()
        {
            // Arrange
            _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(false);

            // Act
            var cut = _ctx.RenderComponent<Perfil>();

            // Assert
            Assert.IsTrue(cut.Markup.Contains("Você não está logado"));
            Assert.IsTrue(cut.Markup.Contains("Logar"));
            Assert.IsTrue(cut.Markup.Contains("Cadastrar"));
        }

        //[Test]
        //public async Task Perfil_LoggedInAsAluno_DisplaysAlunoProfile()
        //{
        //    // Arrange
        //    var alunoId = 1;
        //    var alunoResponse = new AlunoResponse(alunoId, "Test Aluno", DateTime.Now, 0, "contact@test.com", "test@test.com", null, null);
        //    var userSessionData = new PessoaAuthResponse(1, "aluno", false, alunoId, null, "token");

        //    _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(true);
        //    _mockUserSessionService.Setup(s => s.CurrentUser).Returns(userSessionData);
        //    _mockAlunosAPI.Setup(api => api.GetAlunosbyIDAsync(alunoId)).ReturnsAsync(alunoResponse);

        //    // Act
        //    var cut = _ctx.RenderComponent<Perfil>();
        //    await Task.Delay(100); // Allow OnInitializedAsync to complete

        //    // Assert
        //    Assert.IsTrue(cut.Markup.Contains("Perfil do Aluno"));
        //    Assert.IsTrue(cut.Markup.Contains("Test Aluno"));
        //    _mockAlunosAPI.Verify(api => api.GetAlunosbyIDAsync(alunoId), Times.Once);
        //}

        //[Test]
        //public async Task Perfil_LoggedInAsProfessor_DisplaysProfessorProfile()
        //{
        //    // Arrange
        //    var professorId = 1;
        //    var professorResponse = new ProfessorResponse(professorId, "Test Professor", "Math", "contact@prof.com", null, null);
        //    var userSessionData = new PessoaAuthResponse(1, "professor", true, null, professorId, "token");

        //    _mockUserSessionService.Setup(s => s.IsLoggedIn).Returns(true);
        //    _mockUserSessionService.Setup(s => s.CurrentUser).Returns(userSessionData);
        //    _mockProfessoresAPI.Setup(api => api.GetProfessorbyidAsync(professorId)).ReturnsAsync(professorResponse);

        //    // Act
        //    var cut = _ctx.RenderComponent<Perfil>();
        //    await Task.Delay(100); // Allow OnInitializedAsync to complete

        //    // Assert
        //    Assert.IsTrue(cut.Markup.Contains("Perfil do Professor"));
        //    Assert.IsTrue(cut.Markup.Contains("Test Professor"));
        //    _mockProfessoresAPI.Verify(api => api.GetProfessorbyidAsync(professorId), Times.Once);
        //}
    }
}
