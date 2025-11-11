
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
            _app.AddEndpointsAtividades();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAtividades_ReturnsOk()
        {
            // Arrange
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/atividade");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetAtividadeById_ReturnsOk()
        {
            // Arrange
            var atividade = new TbAtividade { IdAtividade = 1, NomeAtividade = "Test Atividade" };
            _context.TbAtividades.Add(atividade);
            await _context.SaveChangesAsync();
            var client = _app.GetTestClient();

            // Act
            var response = await client.GetAsync("/atividade/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateAtividade_ReturnsCreated()
        {
            // Arrange
            var client = _app.GetTestClient();
            var request = new AtividadeCreateRequest(0,"New Atividade",0);
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/atividade", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
