using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_menager.Controllers
{
    public class RadnaMjestaController : Controller
    {
        private readonly AppDBContext _context;
        public RadnaMjestaController(AppDBContext context)
        {
            _context=context;
        }

        // GET: RadnaMjestaController
        public ActionResult Index()
        {
            var radnaMjesta= _context.RadnaMjesta.
                Include(rm=>rm.Odjel).ToList();
            return View(radnaMjesta);
        }

        // GET: RadnaMjestaController/Details/5
        public ActionResult Details(int id)
        {
            RadnoMjesto? rm = _context.RadnaMjesta.FirstOrDefault(r => r.Id == id);
            if (rm != null) return View(rm);
            else
                return NotFound();
        }

        // GET: RadnaMjestaController/Create
        public ActionResult Create()
        {
            ViewBag.Odjeli= _context.Odjeli.ToList();
            return View();
        }

        // POST: RadnaMjestaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind("Id, Naziv,OdjelId")] RadnoMjesto radnoMjesto)
        {
            if (radnoMjesto.OdjelId == 0) radnoMjesto.OdjelId = null;
            if (ModelState.IsValid)
            {
                // Add the new RadnoMjesto to the database
                _context.RadnaMjesta.Add(radnoMjesto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag.Odjeli if the form is invalid
            ViewBag.Odjeli = _context.Odjeli.ToList();
            return View(radnoMjesto);
        }

        // GET: RadnaMjestaController/Edit/5
        public ActionResult Edit(int id)
        {
            var radnoMjesto = _context.RadnaMjesta.FirstOrDefault(rm => rm.Id == id);
            var odjeli = _context.Odjeli.ToList();
            ViewBag.Odjeli=odjeli;
            if (radnoMjesto == null)
            {
                return NotFound();
            }


            return View(radnoMjesto);
        }

        // POST: RadnaMjestaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id, Naziv, OdjelId")] RadnoMjesto radnoMjesto)
        {
            if (id != radnoMjesto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //ako se ne odabere odjel onda je null
                    if (radnoMjesto.OdjelId == 0) radnoMjesto.OdjelId = null; 
                    else
                    {
                        var odjel = _context.Odjeli.FirstOrDefault(o => o.Id == radnoMjesto.OdjelId);
                        if (odjel != null)
                        {
                            radnoMjesto.OdjelId = odjel.Id;
                        }
                    }
                
                    _context.Update(radnoMjesto);
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

            return View(radnoMjesto);
        }

        // GET: RadnaMjestaController/Delete/5
        public ActionResult Delete(int id)
        {
            RadnoMjesto? rm = _context.RadnaMjesta.FirstOrDefault(r => r.Id == id);
           
            if (rm != null) return View(rm);
            else
                return NotFound();
        }

        // POST: RadnaMjestaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var rm = _context.RadnaMjesta.FirstOrDefault(r => r.Id == id);

                if (rm == null)
                {
                    return NotFound();
                }

                _context.RadnaMjesta.Remove(rm);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
