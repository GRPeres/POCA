
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            _app.AddEndpointsQuestoes();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetQuestoes_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/questoes");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetQuestaoById_ReturnsOk()
        {
            // Arrange
            var questao = new TbQuesto { IdQuestao = 1, EnunciadoQuestao = "Test Questao" };
            _context.TbQuestoes.Add(questao);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/questoes/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateQuestao_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new QuestaoRequest
            {
                EnunciadoQuestao = "New Questao",
                RespostaCertaQuestao = "A",
                RespostaErrada1Questao = "B",
                RespostaErrada2Questao = "C",
                RespostaErrada3Questao = "D",
                DificuldadeQuestao = "Fácil",
                TemaQuestao = "Teoria"
            };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/questoes", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
