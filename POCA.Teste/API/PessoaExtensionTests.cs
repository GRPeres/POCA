using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using POCA.API.EndPoints;
using POCA.API.Requests.Pessoa;
using POCA.Banco.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace POCA.Teste.API
{
    [TestFixture]
    public class PessoaExtensionTests
    {
        private WebApplication _app;
        private DbPocaContext _context;

        [SetUp]
        public async Task Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

            // ✅ run in memory TestServer mode
            builder.WebHost.UseTestServer();

            // ✅ DI setup
            builder.Services.AddDbContext<DbPocaContext>(opt =>
                opt.UseInMemoryDatabase("TestDatabase"));
            builder.Services.AddEndpointsApiExplorer();

            // ✅ build app
            _app = builder.Build();

            // ✅ register endpoints
            _app.AddEndpointsPessoas();

            // ✅ start pipeline so routing works
            await _app.StartAsync();

            // ✅ resolve DB context from app
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
        public async Task GetPessoas_ReturnsOk()
        {
            var client = _app.GetTestClient();
            var response = await client.GetAsync("/pessoas");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetPessoaById_ReturnsOk()
        {
            var pessoa = new TbPessoa
            {
                IdPessoa = 1,
                LoginPessoa = "test",
                SenhaPessoa = "test",
            };

            _context.TbPessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();
            var response = await client.GetAsync("/pessoas/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreatePessoa_ReturnsCreated()
        {
            var client = _app.GetTestClient();

            var request = new PessoaCreateRequest("newuser", "password", false, 1, null);
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/pessoas", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task Login_ReturnsOk()
        {
            var pessoa = new TbPessoa
            {
                LoginPessoa = "testuser",
                SenhaPessoa = "password"
            };

            _context.TbPessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            var client = _app.GetTestClient();

            var request = new PessoaLoginRequest("testuser", "password");
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/pessoas/login", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
