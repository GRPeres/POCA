
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using POCA.Banco.Model;

namespace POCA.Teste.Banco
{
    [TestFixture]
    public class DbPocaContextTests
    {
        private DbContextOptions<DbPocaContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DbPocaContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public void DbPocaContext_CanBeInstantiated()
        {
            using (var context = new DbPocaContext(_options))
            {
                Assert.IsNotNull(context);
            }
        }

        [Test]
        public void DbPocaContext_CanAddAndSaveChanges()
        {
            using (var context = new DbPocaContext(_options))
            {
                var pessoa = new TbPessoa { LoginPessoa = "testuser", SenhaPessoa = "password" };
                context.TbPessoas.Add(pessoa);
                context.SaveChanges();

                Assert.AreEqual(1, context.TbPessoas.Count());
            }
        }
    }
}
