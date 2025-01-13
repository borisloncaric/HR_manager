using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HR_menager.Controllers
{
    public class ZahtjeviController : Controller
    {
        private readonly AppDBContext _context;
        public ZahtjeviController(AppDBContext context)
        {
            _context = context;
        }


        // GET: ZahtjevController
        public ActionResult Index()
        {
        var zahtjevi = _context.Zahtjevi
        .Include(z => z.Podnositelj) // Učitaj ime i prezime podnositelja
        .Include(z => z.ObradioZaposlenik)
        .Include(z=>z.StatusZahtjeva)// Učitaj ime i prezime osobe koja je obradila zahtjev
        .ToList();
            return View(zahtjevi);
        }

        // GET: ZahtjevController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZahtjevController/Create
        public ActionResult Create()
        {
            var Zaposlenici = _context.Zaposlenici
              .Select(z => new { z.Id, ImePrezime = z.VratiImePrezime() })
              .ToList();

            var StatusiZahtjeva = _context.StatusiZahtjeva
                .Select(s => new { s.Id, s.Status })
                .ToList();

            ViewBag.Zaposlenici = new SelectList(Zaposlenici, "Id", "ImePrezime");
            ViewBag.StatusiZahtjeva = new SelectList(StatusiZahtjeva, "Id", "Status");
            return View();
        }

        // GET: ZahtjevController/Edit/5
        public ActionResult Edit(int id)
        {
            var zahtjev = _context.Zahtjevi
             .Include(z => z.Podnositelj)
             .Include(z => z.StatusZahtjeva)
             .Include(z => z.ObradioZaposlenik)
             .FirstOrDefault(z => z.Id == id);

            if (zahtjev == null)
            {
                return NotFound();
            }
          
            var Zaposlenici = _context.Zaposlenici
                .Select(z => new { z.Id, ImePrezime = z.VratiImePrezime() })
                .ToList();

            var StatusiZahtjeva = _context.StatusiZahtjeva
                .Select(s => new { s.Id, s.Status })
                .ToList();

            ViewBag.Zaposlenici = new SelectList(Zaposlenici, "Id", "ImePrezime");
            ViewBag.StatusiZahtjeva = new SelectList(StatusiZahtjeva, "Id", "Status");
            return View(zahtjev);
        }

        // GET: ZahtjevController/Delete/5
        public ActionResult Delete(int id)
        {
            var zahtjev = _context.Zahtjevi
              .Include(z => z.Podnositelj)
              .ThenInclude(z=>z.Radnomjesto)
              .ThenInclude(z=>z.Odjel)
              .Include(z => z.StatusZahtjeva)
              .Include(z => z.ObradioZaposlenik)
              .FirstOrDefault(z => z.Id == id);

            if (zahtjev == null)
            {
                return NotFound();
            }
            return View(zahtjev);
        }



        // POST: ZahtjevController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PodnositeljId, PocetniDatum, KrajnjiDatum, DatumZahtjeva, Razlog, StatusZahtjevaId,ObradioZaposlenikId")] Zahtjev zahtjev)
        {
            if (zahtjev.ObradioZaposlenikId == 0) zahtjev.ObradioZaposlenikId = null;
          
            if (ModelState.IsValid)
            {
               _context.Zahtjevi.Add(zahtjev);
               _context.SaveChanges();
               return RedirectToAction("Index");
            }

            var Zaposlenici = _context.Zaposlenici
                .Select(z => new { z.Id, ImePrezime = z.VratiImePrezime() })
                .ToList();

            var StatusiZahtjeva = _context.StatusiZahtjeva
                .Select(s => new { s.Id, s.Status })
                .ToList();

            ViewBag.Zaposlenici = new SelectList(Zaposlenici, "Id", "ImePrezime");
            ViewBag.StatusiZahtjeva = new SelectList(StatusiZahtjeva, "Id", "Status");
            return View(zahtjev);

        }


        // POST: ZahtjevController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id, PodnositeljId, PocetniDatum, KrajnjiDatum, DatumZahtjeva, Razlog, StatusZahtjevaId,ObradioZaposlenikId")] Zahtjev zahtjev)
        {
            if(id != zahtjev.Id) return NotFound();
            if(zahtjev.ObradioZaposlenikId==0)zahtjev.ObradioZaposlenikId=null;
            if (ModelState.IsValid)
            {
                try
                {
                    // Update Zaposlenik in the database
                    _context.Update(zahtjev);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Zaposlenici.Any(z => z.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Greška kod spremanja");
                    }
                }


                return RedirectToAction(nameof(Index));
            }

            var zah = _context.Zahtjevi
                 .Include(z => z.Podnositelj)
                 .Include(z => z.StatusZahtjeva)
                 .Include(z => z.ObradioZaposlenik)
                 .FirstOrDefault(z => z.Id == id);


            var Zaposlenici = _context.Zaposlenici
                .Select(z => new { z.Id, ImePrezime = z.VratiImePrezime() })
                .ToList();

            var StatusiZahtjeva = _context.StatusiZahtjeva
                .Select(s => new { s.Id, s.Status })
                .ToList();

            ViewBag.Zaposlenici = new SelectList(Zaposlenici, "Id", "ImePrezime");
            ViewBag.StatusiZahtjeva = new SelectList(StatusiZahtjeva, "Id", "Status");
            return View(zahtjev);

        }


        // POST: ZahtjevController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var zahtjev = _context.Zahtjevi.FirstOrDefault(z => z.Id == id);

                if (zahtjev == null)
                {
                    return NotFound();
                }

                _context.Zahtjevi.Remove(zahtjev);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
