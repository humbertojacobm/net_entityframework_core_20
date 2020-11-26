using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");

            using(var context = new SamuraiContext(builder.Options))
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Assert.AreNotEqual(0, samurai.Id);

            }
        }

        [TestMethod]
        public void CanInsertSamuraiIntoDatabase2()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");

            using (var context = new SamuraiContext(builder.Options))
            {
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);

            }
        }
    }
}
