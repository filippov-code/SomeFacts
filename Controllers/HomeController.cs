using Microsoft.AspNetCore.Mvc;
using SomeFacts.Domains;
using Microsoft.EntityFrameworkCore;

namespace SomeFacts.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext dbcontext;

        public HomeController(AppDbContext context)
        {
            dbcontext = context;
        }

        public IActionResult Index()
        {
            return View( "Index", new Random().Next(0, 100).ToString() );
        }

        public JsonResult GetFacts(int count)
        {
            var rand = new Random();
            Fact[] facts = new Fact[count];
            for (int i = 0; i < count; i++)
            {
                facts[i] = new Fact{ Id = rand.Next(0, 2000), Text = GetRandomString() };
            }

            return Json(facts);
        }

        public JsonResult GetFactFromDB()
        {
            int toSkip = new Random().Next(0, dbcontext.Facts.Count());
            Fact fact = dbcontext.Facts.Skip(toSkip).Take(1).First();
            
            return Json(fact);
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
