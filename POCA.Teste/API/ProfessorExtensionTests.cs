using Microsoft.AspNetCore.Builder;
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
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ Enable TestServer
            builder.WebHost.UseTestServer();

            // ✅ DI config
            builder.Services.AddDbContext<DbPocaContext>(opt =>
                opt.UseInMemoryDatabase("TestDatabase"));
            builder.Services.AddEndpointsApiExplorer();

            // ✅ Build app
            _app = builder.Build();

            // ✅ Map endpoints
            _app.AddEndpointsProfessores();

            // ✅ Start HTTP pipeline
            await _app.StartAsync();

            // ✅ Resolve db
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
        public async Task GetProfessores_ReturnsOk()
        {
            var client = _app.GetTestClient();
            var response = await client.GetAsync("/professores");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetProfessorById_ReturnsOk()
        {
            var professor = new TbProfessore
            {
                IdProfessor = 1,
                NomeProfessor = "Test Professor",
                ContatoProfessor = "123456789",
                MateriaProfessor = "Math"
            };

            _context.TbProfessores.Add(professor);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/professores/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateProfessor_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new ProfessorCreateRequest(
                "New Professor",
                "Test Materia",
                "123456789"
            );

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/professores", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
