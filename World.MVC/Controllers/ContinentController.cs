using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.MVC.Contracts;
using World.MVC.Models;
using World.MVC.Services;

namespace World.MVC.Controllers
{
    public class ContinentController : Controller
    {
        private readonly IContinentService _continentService;

        public ContinentController(IContinentService continentService)
        {
            _continentService = continentService;
        }
        // GET: ContinentController
        public async Task<ActionResult> Index()
        {
            var model = await _continentService.GetAll(new SearchDTO { Filters = new Dictionary<string, string>(), PageIndex = -1, PageSize = 0 });
            return View(model);
        }

        // GET: ContinentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _continentService.GetById(id);
            return View(model);
        }

        // GET: ContinentController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ContinentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateContinentVM createContinent)
        {
            try
            {
                var response = await _continentService.CreateContinent(createContinent);

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(createContinent);
        }

        // GET: ContinentController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _continentService.GetById(id);
            return View(model);
        }

        // POST: ContinentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContinentVM continent)
        {
            try
            {
                var response = await _continentService.UpdateContinent(continent);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(continent);
        }

        // POST: ContinentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _continentService.DeleteContinent(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();
        }
    }
}
