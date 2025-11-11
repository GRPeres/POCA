
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.EndPoints;
using POCA.API.Requests.Professor;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class ProfessorExtensionTests
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
            _app.AddEndpointsProfessores();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetProfessores_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/professores");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetProfessorById_ReturnsOk()
        {
            // Arrange
            var professor = new TbProfessore { IdProfessor = 1, NomeProfessor = "Test Professor" };
            _context.TbProfessores.Add(professor);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/professores/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateProfessor_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new ProfessorCreateRequest("New Professor", "Test Materia", "123456789");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/professores", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
