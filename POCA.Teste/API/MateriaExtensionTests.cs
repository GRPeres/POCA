
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.EndPoints;
using POCA.API.Requests.Materia;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class MateriaExtensionTests
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

            _app = WebApplication.Create(new WebApplicationOptions());
            _app.AddEndpointsMaterias();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetMaterias_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/materias");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetMateriaById_ReturnsOk()
        {
            // Arrange
            var materia = new TbMateria { IdMateria = 1, NomeMateria = "Test Materia" };
            _context.TbMaterias.Add(materia);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/materias/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateMateria_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new MateriaRequest("New Materia");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/materias", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
