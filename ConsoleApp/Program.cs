using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            //AddSamurai();
            //GetSamurais("After Add:");
            //InsertMultipleSamurais();
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //MultipleDatabaseOperations();
            //RetrieveAndDeleteASamurai();
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //InsertNewSamuraiWithAQuote();
            //InsertNewSamuraiWithManyQuotes();
            //AddQuoteToExistingSamuraiWhileTracked();
            //AddQuoteToExistingSamuraiNotTracked(3);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraisWithQuotes();
            //ExplicitLoadQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoABattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithHorse();
            //AddNewHorseToSamuraiUsingId();
            //AddNewHorseToSamuraiObject();
            //AddNewHorseToDisconnectedSamuraiObject();
            //ReplaceAHorse();
            //ReplaceHorse2();
            //GetSamuraisWithHorse();
            //GetHorseWithSamurai();
            //GetSamuraiWithClan();
            //GetClanWithSamurais();
            //QuerySamuraiBattleStats();
            //QueryUsingRawSql();
            //QueryUsingRawSqlWithInterpolation();
            //DANGERDANGERQueryUsingRawSqlWithInterpolation();
            //QueryUsingFromRawSqlStoredProc();
            //InterpolatedRawSqlQueryStoredProc();
            ExecuteSomeRawSql();
            //Console.Write("Press any key...");
            //Console.ReadKey();
        }
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Sampson" };
            var samurai2 = new Samurai { Name = "Tasha" };
            var samurai3 = new Samurai { Name = "Nick" };
            var samurai4 = new Samurai { Name = "Vanessa" };
            _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }
        private static void InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Kikuchio" };
            var clan = new Clan { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Simpson" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamuraisSimpler()
        {
            //var samurais = context.Samurais.ToList();
            var query = _context.Samurais;
            //var samurais = query.ToList();
            foreach(var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void QueryFilters()
        {
            var name = "Sampson";
            //var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
            //var filter = "J%";
            //var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, filter)).ToList();
            //var samurais = _context.Samurais.Where(s => s.Name == name).FirstOrDefault();
            //var samurais = _context.Samurais.FirstOrDefault(s => s.Name == name);
            //var samurais = _context.Samurais.Find(2);
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Kikuchiyo" });
            _context.SaveChanges();
        }

        private static void RetrieveAndDeleteASamurai()
        {
            var samurai = _context.Samurais.Find(2);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name=" Battle of Okehazama",
                StartDate = new DateTime(1560,05,01),
                EndDate= new DateTime(1560,06,15)
            });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text="I've come to save you"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithManyQuotes()
        {
            var samuari = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote { Text="What out for my..."},
                    new Quote {Text="I told you to watch out for the sharp"}
                }
            };
            _context.Samurais.Add(samuari);
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you"
            });
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });
            using(var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }
        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });
            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            //var samuariWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
            //var samuariWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Julie"))
            //    .Include(s => s.Quotes).FirstOrDefault();
            var samuariWithQuotes = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Julie"));

        }

        private static void ProjectSomeProperties()
        {
            //var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }

        private static void ProjectSamuraisWithQuotes()
        {
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(s => new { s.Id, s.Name, s.Quotes })
            //    .ToList();
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(s => new { s.Id, s.Name, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) })
            //    .ToList();
            var samuraisWithHappQuotes = _context.Samurais
                .Select(s => new { Samurai = s, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) })
                .ToList();
            var firstsamurai = samuraisWithHappQuotes[0].Samurai.Name += " The happiest";


        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("SampsonSan"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void FilteringWithRelatedData()
        {
            var samurai = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 3);
            samurai.Quotes[0].Text = "Did you hear that?";
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 3);
            var quote = samurai.Quotes[0];
            quote.Text += "Did you hear that?";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles
                .Add(new SamuraiBattle { SamuraiId = 11 });
            _context.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 1 };
            _context.Remove(join);
            _context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 2);

            var samuraiWithBattlesCleaner = _context.Samurais.Where(s => s.Id == 2)
                .Select(s => new
                {
                    Samurai = s,
                    Battle = s.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Scout", SamuraiId = 3 };
            _context.Add(horse);
            _context.SaveChanges();
        }

        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(11);
            samurai.Horse = new Horse { Name = "Black Beauty" };
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisconnectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 10);
            samurai.Horse = new Horse { Name = "Mr. Ed" };
            using( var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void ReplaceAHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Id == 10);
            samurai.Horse = new Horse { Name = "Trigger" };
            _context.SaveChanges();
        }

        private static void ReplaceHorse2()
        {
            var samurai = _context.Samurais.Find(10);
            samurai.Horse = new Horse { Name = "Triggerii" };
            _context.SaveChanges();
        }

        private static void GetSamuraisWithHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).ToList();

        }

        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);

            var horseWithSamurai = _context.Samurais.Include(s => s.Horse)
                .FirstOrDefault(s => s.Horse.Id == 3);

            var horsesWithSamurais = _context.Samurais
                .Where(s => s.Horse != null)
                .Select(s => new { Horse = s.Horse, Samurai = s })
                .ToList();
        }

        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clan).FirstOrDefault();
        }

        private static void GetClanWithSamurais()
        {
            var clan = _context.Clans.Find(3);
            var samuraisForClan = _context.Samurais.Where(s => s.Clan.Id == 1).ToList();
        }

        private static void QuerySamuraiBattleStats()
        {
            //var stats = _context.SamuraiBattleStats.ToList();
            //var firstStat = _context.SamuraiBattleStats.FirstOrDefault();
            //var sampsonStat = _context.SamuraiBattleStats
            //    .Where(s => s.Name == "SampsonSan").FirstOrDefault();
            var findone = _context.SamuraiBattleStats.Find(2);
        }

        private static void QueryUsingRawSql()
        {
            //var samurais = _context.Samurais.FromSqlRaw("select * from Samurais").ToList();
            //var samurais2 = _context.Samurais.FromSqlRaw("select Id, Name from Samurais").ToList();

            var samurais = _context.Samurais.FromSqlRaw("Select Id, Name, ClanId from Samurais").Include(s => s.Quotes)
                .ToList();
        }

        private static void QueryUsingRawSqlWithInterpolation()
        {
            string name = "Kikuchyo";
            var samurais = _context.Samurais
                .FromSqlInterpolated($"Select * from Samurais Where Name = {name}")
                .ToList();
        }

        private static void DANGERDANGERQueryUsingRawSqlWithInterpolation()
        {
            string name = "Kikuchyo";
            var samurais = _context.Samurais
                .FromSqlRaw($"Select * from Samurais Where Name = '{name}'")
                .ToList();

        }

        private static void QueryUsingFromRawSqlStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlRaw(
                $"EXEC dbo.SamuraisWhoSaidAWord {0}", text
                ).ToList();
        }

        private static void InterpolatedRawSqlQueryStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlInterpolated(
                $"EXEC dbo.SamuraisWhoSaidAWord {text}"
                ).ToList();
        }

        private static void ExecuteSomeRawSql()
        {
            var samuraiId = 22;
            //var x = _context.Database
            //    .ExecuteSqlRaw($"EXEC dbo.DeleteQuotesForSamurai {0}", samuraiId);

            var x = _context.Database
                .ExecuteSqlInterpolated($"EXEC dbo.DeleteQuotesForSamurai {samuraiId}");

        }

    }
}
