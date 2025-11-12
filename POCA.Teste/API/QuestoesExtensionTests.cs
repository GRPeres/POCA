using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.Endpoints;
using POCA.API.Requests.NewFolder;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class QuestoesExtensionTests
    {
        private WebApplication _app;
        private DbPocaContext _context;

        [SetUp]
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ Enable TestServer
            builder.WebHost.UseTestServer();

            // ✅ Service config
            builder.Services.AddDbContext<DbPocaContext>(opt =>
                opt.UseInMemoryDatabase("TestDatabase"));

            builder.Services.AddEndpointsApiExplorer();

            // ✅ Build app
            _app = builder.Build();

            // ✅ Map endpoints
            _app.AddEndpointsQuestoes();

            // ✅ Start app
            await _app.StartAsync();

            // ✅ Get DB instance
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
        public async Task GetQuestoes_ReturnsOk()
        {
            var client = _app.GetTestClient();
            var response = await client.GetAsync("/questoes");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetQuestaoById_ReturnsOk()
        {
            var questao = new TbQuesto
            {
                IdQuestao = 1,
                EnunciadoQuestao = "Test Questao",
                RespostacertaQuestao = "Correct Answer",
                Respostaerrada1Questao = "Wrong 1",
                Respostaerrada2Questao = "Wrong 2",
                Respostaerrada3Questao = "Wrong 3",
                DificuldadeQuestao = "Fácil",
                TemaQuestao = "Teoria"
            };

            _context.TbQuestoes.Add(questao);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/questoes/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateQuestao_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new QuestaoRequest(
                "New Questao",
                "A",
                "B",
                "C",
                "D",
                "Fácil",
                "Teoria"
            );

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/questoes", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
