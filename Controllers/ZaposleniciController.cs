using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HR_menager.Controllers
{
    public class ZaposleniciController : Controller
    {
        private readonly AppDBContext _context;
        public ZaposleniciController(AppDBContext context)
        {
            _context = context;
        }


        // GET: Zaposlenici
        public ActionResult Index()
        {
            // Učitaj zaposlenike zajedno s radnim mjestom i odjelom
            var zaposlenici = _context.Zaposlenici
                .Include(z => z.Radnomjesto) 
                .ThenInclude(rm => rm.Odjel) 
                .ToList();

            return View(zaposlenici);

        }

        // GET: Zaposlenici/Details/5
        public ActionResult Details(int id)
        {
            Zaposlenik? zap = _context.Zaposlenici.FirstOrDefault(zap=>zap.Id==id);
            if(zap != null)
                return View(zap);
            else return NotFound();
        }

        // GET: Zaposlenici/Delete/5
        public ActionResult Delete(int id)
        {
            Zaposlenik? zap = _context.Zaposlenici.FirstOrDefault(zap => zap.Id == id);
            if (zap == null) return NotFound();
            if (zap.RadnoMjestoId != null)
                ViewBag.RadnoMjesto = _context.RadnaMjesta.FirstOrDefault(rm => rm.Id == zap.RadnoMjestoId)?.Naziv ?? "Greška kod dohvaćanja";
            else 
                ViewBag.RadnoMjesto = "Nema radno mjesto";
            return View(zap);

        }

        // GET: Zaposlenici/Create
        public ActionResult Create()
        {
            
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
              .Include(rm=>rm.Odjel)
                .ToList();

            // Prosljedi listu
            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "Id", "Naziv");
            return View();
        }

        // GET: Zaposlenici/Edit/5
        public ActionResult Edit(int id)
        {
            var zaposlenik = _context.Zaposlenici
                .Include(z => z.Radnomjesto)
                .ThenInclude(rm => rm.Odjel)
                .FirstOrDefault(z => z.Id == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }

            // dropdown
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .Include(rm => rm.Odjel)
                .Select(rm => new
                {
                    RadnoMjestoId = rm.Id,
                    CombinedName = rm.Odjel != null
                        ? $"{rm.Naziv} ({rm.Odjel.Naziv})"
                        : $"{rm.Naziv} (nema odjel)"
                })
                .ToList();

            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "RadnoMjestoId", "CombinedName", zaposlenik.RadnoMjestoId);

            return View(zaposlenik);
        }


        // POST: Zaposlenici/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Ime, Prezime, BrojTelefona, RadnoMjestoId")] Zaposlenik zaposlenik)
        {
            if (zaposlenik.RadnoMjestoId == 0) zaposlenik.RadnoMjestoId = null;
            if (ModelState.IsValid)
            {
              
                _context.Zaposlenici.Add(zaposlenik);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            var radnaMjestaWithOdjeli = _context.RadnaMjesta
             .Include(rm => rm.Odjel) 
             .Select(rm => new
             {
                 Id = rm.Id,
                 Naziv = rm.Odjel != null
                     ? $"{rm.Naziv} ({rm.Odjel.Naziv})"
                     : $"{rm.Naziv} (nema odjel)"
             })
             .ToList();

            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "Id", "Naziv");

            return View(zaposlenik);
        }

        // POST: Zaposlenici/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id, Ime, Prezime, BrojTelefona, RadnoMjestoId")] Zaposlenik zaposlenik)
        {
            if (id != zaposlenik.Id)
            {
                return NotFound();
            }
            if (zaposlenik.RadnoMjestoId == 0) zaposlenik.RadnoMjestoId = null;
            if (ModelState.IsValid)
            {
                try
                {
                    // Update Zaposlenik in the database
                    _context.Update(zaposlenik);
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
                        ModelState.AddModelError("","Greška kod spremanja");
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            var radnaMjestaWithOdjeli = _context.RadnaMjesta
            .Include(rm => rm.Odjel)
            .Select(rm => new
            {
                RadnoMjestoId = rm.Id,
                CombinedName = rm.Odjel != null
                    ? $"{rm.Naziv} ({rm.Odjel.Naziv})"
                    : $"{rm.Naziv} (nema odjel)"
            })
            .ToList();

            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "RadnoMjestoId", "CombinedName", zaposlenik.RadnoMjestoId);

            return View(zaposlenik);
        }



        // POST: Zaposlenici/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var zap = _context.Zaposlenici.FirstOrDefault(z => z.Id == id);

                if (zap == null)
                {
                    return NotFound();
                }

                _context.Zaposlenici.Remove(zap);  
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
