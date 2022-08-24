using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomeFacts.Data;
using SomeFacts.Models;

namespace SomeFacts.Controllers
{
    public class FactsController : Controller
    {
        AppDbContext dbContext;

        public FactsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public JsonResult GetFacts(int count)
        //{
        //    var rand = new Random();
        //    Fact[] facts = new Fact[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        facts[i] = new Fact { Id = rand.Next(0, 2000), Text = GetRandomString() };
        //    }

        //    return Json(facts);
        //}

        public JsonResult GetFacts(int count = 1)
        {
            int toSkip = new Random().Next(0, dbContext.Facts.Count());
            Fact fact = dbContext.Facts.Skip(toSkip).Take(1).First();

            Fact[] facts = { fact };
            return Json(facts);
        }

        private string GetRandomString()
        {
            string res = "qwertyuiopasdfghklzxcvbnm";

            res += res.ToUpper();

            res += "1234567890";

            Random rand = new Random();
            int randLength = rand.Next(5, 10);
            char[] chars = new char[randLength];
            for (int i = 0; i < randLength; i++)
            {
                chars[i] = res[rand.Next(0, res.Length)];
            }
            return new string(chars);
        }
    }
}
