using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
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
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ Use Test Server
            builder.WebHost.UseTestServer();

            // ✅ DI setup
            builder.Services.AddDbContext<DbPocaContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            builder.Services.AddEndpointsApiExplorer();

            // ✅ Build app
            _app = builder.Build();

            // ✅ Register endpoints
            _app.AddEndpointsMaterias();

            // ✅ Start pipeline so routing works
            await _app.StartAsync();

            _context = _app.Services.GetRequiredService<DbPocaContext>();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.Dispose();
            await _app.DisposeAsync();
        }

        [Test]
        public async Task GetMaterias_ReturnsOk()
        {
            var client = _app.GetTestClient();

            var response = await client.GetAsync("/materias");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetMateriaById_ReturnsOk()
        {
            var materia = new TbMateria
            {
                IdMateria = 1,
                NomeMateria = "Test Materia"
            };

            _context.TbMaterias.Add(materia);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/materias/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateMateria_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new MateriaRequest
            {
                NomeMateria = "New Materia"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/materias", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
