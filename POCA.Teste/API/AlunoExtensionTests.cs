
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.EndPoints;
using POCA.API.Requests.Aluno;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class AlunoExtensionTests
    {
        private WebApplication _app;
        private DbPocaContext _context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DbPocaContext>(options =>
                options.UseInMemoryDatabase(databaseName: "TestDatabase"));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<DbPocaContext>();

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());
            _app = builder.Build();
            _app.AddEndpointsAlunos();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _app.DisposeAsync();

        }

        [Test]
        public async Task GetAlunos_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/alunos");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetAlunoById_ReturnsOk()
        {
            // Arrange
            var aluno = new TbAluno { IdAluno = 1, NomeAluno = "Test Aluno" };
            _context.TbAlunos.Add(aluno);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/alunos/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateAluno_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new AlunoCreateRequest("New Aluno", DateTime.Now, 0, "123456789", "test@test.com");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/alunos", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
