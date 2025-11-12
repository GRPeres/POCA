using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.EndPoints;
using POCA.API.Requests.Resposta;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class RespostaExtensionTests
    {
        private WebApplication _app;
        private DbPocaContext _context;

        [SetUp]
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ Use TestServer
            builder.WebHost.UseTestServer();

            // ✅ Add in-memory DB
            builder.Services.AddDbContext<DbPocaContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            builder.Services.AddEndpointsApiExplorer();

            // ✅ Build app
            _app = builder.Build();

            // ✅ Register endpoints
            _app.AddEndpointsRespostas();

            // ✅ Start app
            await _app.StartAsync();

            // ✅ Get DB context from DI
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
        public async Task GetRespostas_ReturnsOk()
        {
            var client = _app.GetTestClient();
            var response = await client.GetAsync("/respostas");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetRespostaById_ReturnsOk()
        {
            var resposta = new TbResposta
            {
                IdResposta = 1,
                FinalResposta = "Test Resposta"
            };

            _context.TbRespostas.Add(resposta);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/respostas/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateResposta_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new RespostaRequest(
                "New Resposta",
                1,
                1,
                1
            );

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/respostas", content);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
