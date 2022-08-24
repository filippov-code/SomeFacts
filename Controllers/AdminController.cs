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

        [HttpPost]
        public async Task<IActionResult> AddNewFact(NewFactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var newFact = new Fact { Text = model.Text };

            await dbContext.Facts.AddAsync(newFact);

            dbContext.SaveChanges();

            return View("Index");
        }
    }
}
