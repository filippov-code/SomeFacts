using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeFacts.Data;
using SomeFacts.Models;
using SomeFacts.ViewModels;

namespace SomeFacts.Controllers
{
    [Authorize(Policy = RoleNames.Administrator)]
    public class AdminController : Controller
    {
        AppDbContext dbContext;

        public AdminController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewFact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFact(NewFactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newFact = new Fact { Text = model.Text };

            await dbContext.Facts.AddAsync(newFact);

            dbContext.SaveChanges();

            return RedirectToAction("AddNewFact", "Admin");
        }

        public IActionResult AllFacts()
        {
            var facts = dbContext.Facts.ToArray().Reverse();
            return View(facts);
        }

        [HttpDelete]
        public async Task DeleteFact(int id)
        {
            var fact = new Fact { Id = id };
            dbContext.Facts.Remove(fact);
            await dbContext.SaveChangesAsync();
        }

        public IActionResult EditFact(int id)
        {
            var fact = dbContext.Facts.SingleOrDefault(x => x.Id == id);
            return View(fact);
        }

        [HttpPost]
        public async Task<IActionResult> EditFact(Fact fact)
        {
            if (!ModelState.IsValid)
            {
                return View(fact);
            }

            dbContext.Facts.Update(fact);
            dbContext.SaveChanges();

            return RedirectToAction("AllFacts", "Admin");
        }
    }
}
