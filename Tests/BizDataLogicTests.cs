using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddMultipleSamurais");
            using(var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic(context);
                var nameList = new string[] {"kikuchiyo","Kyuzo","Rikchi" };
                var result = bizlogic.AddMultipleSamurais(nameList);
                Assert.AreEqual(nameList.Count(), result);
            }
        }
        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("InsertNewSamurai");
            using(var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic(context);
                bizlogic.InsertNewSamurai(new Samurai());
            }
            using(var context2 = new SamuraiContext(builder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }

        }
        [TestMethod, TestCategory("SamuraiWithQuotes")]
        public void CanGetSamuraiWithQuotes()
        {
            int samuraiId;
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("SamuraiWithQuotes");
            using (var seedContext = new SamuraiContext(builder.Options))
            {
                var samuraiGraph = new Samurai
                {
                    Name = "Kyuzo",
                    Quotes = new List<Quote>
                    {
                        new Quote { Text = "Watch out for my sharp sword!" },
                        new Quote { Text = "I told you to watch out for the sharp sword! Oh well!" }
                    }
                };
                seedContext.Samurais.Add(samuraiGraph);
                seedContext.SaveChanges();
                samuraiId = samuraiGraph.Id;
            }
            using(var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic(context);
                var result = bizlogic.GetSamuraiWithQuotes(samuraiId);
                Assert.AreEqual(2, result.Quotes.Count);
            }
        }
    }
}
