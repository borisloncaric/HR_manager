using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_menager.Controllers
{
    public class OdjeliController : Controller
    {
        private readonly AppDBContext _context;
        public OdjeliController(AppDBContext context)
        {
            _context=context;
        }

        // GET: OdjeliController
        public ActionResult Index()
        {
            var odjeli=_context.Odjeli.ToList();
            return View(odjeli);
        }


        // GET: OdjeliController/Details/5
        public ActionResult Details(int id)
        {
            Odjel od = _context.Odjeli.FirstOrDefault(odjel => odjel.Id == id);
            if (od != null) return View(od);
            else
                return NotFound();
        }

        // GET: OdjeliController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OdjeliController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind("Id, Naziv")] Odjel odjel)
        {
            if (ModelState.IsValid)
            {
                _context.Odjeli.Add(odjel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(odjel);
        }

        // GET: OdjeliController/Edit/5
        public ActionResult Edit(int id)
        {
            var odjel = _context.Odjeli.FirstOrDefault(o => o.Id == id);

            if (odjel == null)
            {
                return NotFound();
            }


            return View(odjel);
        }

        // POST: OdjeliController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id, Naziv")] Odjel odjel)
        {
            if (id != odjel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(odjel);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Odjeli.Any(o => o.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(odjel);
        }

        // GET: OdjeliController/Delete/5
        public ActionResult Delete(int id)
        {
            Odjel? odj = _context.Odjeli.FirstOrDefault(o => o.Id == id);
            ViewBag.radnaMjesta = _context.RadnaMjesta.Where(rm => rm.OdjelId == odj.Id).ToList();
            if (odj != null) return View(odj);
            else
                return NotFound();
        }

        // POST: OdjeliController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var odj = _context.Odjeli.FirstOrDefault(o => o.Id == id);
            try
            {
                if (odj == null)
                {
                    return NotFound();
                }

                _context.Odjeli.Remove(odj);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewBag.radnaMjesta = _context.RadnaMjesta.Where(rm => rm.OdjelId == odj.Id).ToList();
                return View(odj);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
