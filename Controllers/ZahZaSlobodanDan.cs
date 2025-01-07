using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_menager.Controllers
{
    public class ZahZaSlobodanDan : Controller
    {
        // GET: ZahZaSlobodanDan
        public ActionResult Index()
        {
            return View();
        }

        // GET: ZahZaSlobodanDan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZahZaSlobodanDan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZahZaSlobodanDan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZahZaSlobodanDan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZahZaSlobodanDan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZahZaSlobodanDan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZahZaSlobodanDan/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
