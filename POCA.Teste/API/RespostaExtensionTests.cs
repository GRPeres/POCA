
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            _app.AddEndpointsRespostas();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetRespostas_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/respostas");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetRespostaById_ReturnsOk()
        {
            // Arrange
            var resposta = new TbResposta { IdResposta = 1, FinalResposta = "Test Resposta" };
            _context.TbRespostas.Add(resposta);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/respostas/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateResposta_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new RespostaRequest
            {
                FinalResposta = "New Resposta",
                IdAtividade = 1,
                IdAluno = 1,
                IdQuestao = 1
            };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/respostas", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
