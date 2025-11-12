using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POCA.API.EndPoints;
using POCA.API.Requests.Atividade;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class AtividadeExtensionTests
    {
        private WebApplication _app;
        private DbPocaContext _context;

        [SetUp]
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ Use Test Server
            builder.WebHost.UseTestServer();

            // ✅ Register services
            builder.Services.AddDbContext<DbPocaContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            builder.Services.AddEndpointsApiExplorer();

            _app = builder.Build();

            // ✅ Add API endpoints
            _app.AddEndpointsAtividades();

            // ✅ Run pipeline so routing works
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
        public async Task GetAtividades_ReturnsOk()
        {
            var client = _app.GetTestClient();

            var response = await client.GetAsync("/atividade");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetAtividadeById_ReturnsOk()
        {
            var atividade = new TbAtividade
            {
                IdAtividade = 1,
                NomeAtividade = "Test Atividade"
            };

            _context.TbAtividades.Add(atividade);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/atividade/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateAtividade_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new AtividadeCreateRequest(0, "New Atividade", 1);
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/atividade", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
