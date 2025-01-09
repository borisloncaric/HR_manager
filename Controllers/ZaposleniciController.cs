using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            // Load zaposlenici
            var zaposlenici = _context.Zaposlenici.ToList();

            // Perform a left join between RadnaMjesta and Odjeli
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .GroupJoin(
                    _context.Odjeli,
                    rm => rm.OdjelId,  // Join condition: RadnoMjesto's OdjelId
                    o => o.Id,          // Odjel's Id
                    (rm, odjeli) => new
                    {
                        RadnoMjestoId = rm.Id,
                        CombinedName = rm.OdjelId.HasValue
                            ? $"{rm.Naziv} ({odjeli.FirstOrDefault().Naziv})"  
                            : $"{ rm.Naziv } (nema odjel)"// If no OdjelId
                    }
                )
                .ToList() 
                .ToDictionary(x => x.RadnoMjestoId, x => x.CombinedName); 

            // Pass the dictionary to the ViewBag
            ViewBag.RadnaMjesta = radnaMjestaWithOdjeli;
            return View(zaposlenici);
        }

        // GET: Zaposlenici/Details/5
        public ActionResult Details(int id)
        {
            Zaposlenik zap = _context.Zaposlenici.FirstOrDefault(zap=>zap.Id==id);
            if(zap != null)return View(zap);
            else
            return NotFound();
        }

        // GET: Zaposlenici/Create
        public ActionResult Create()
        {
            // Perform a left join between RadnaMjesta and Odjeli
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .GroupJoin(
                    _context.Odjeli,
                    rm => rm.OdjelId,  // Join condition: RadnoMjesto's OdjelId
                    o => o.Id,         // Odjel's Id
                    (rm, odjeli) => new
                    {
                        Id = rm.Id,  // Ensure this matches the "Id" expected in the view
                        Naziv = rm.OdjelId.HasValue
                            ? $"{rm.Naziv} ({odjeli.FirstOrDefault().Naziv ?? "nema odjel"})"
                            : $"{rm.Naziv} (nema odjel)" // If no OdjelId
                    }
                )
                .ToList();

            // Pass the combined list to the view
            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "Id", "Naziv");
            return View();
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

            // Repopulate dropdown in case of validation error or exception
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .GroupJoin(
                    _context.Odjeli,
                    rm => rm.OdjelId,
                    o => o.Id,
                    (rm, odjeli) => new
                    {
                        Id = rm.Id,
                        Naziv = rm.OdjelId.HasValue
                            ? $"{rm.Naziv} ({odjeli.FirstOrDefault().Naziv ?? "nema odjel"})"
                            : $"{rm.Naziv} (nema odjel)"
                    }
                ).ToList();

            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "Id", "Naziv");

            return View(zaposlenik);
        }

        // GET: Zaposlenici/Edit/5
        public ActionResult Edit(int id)
        {
            // Get the Zaposlenik by id
            var zaposlenik = _context.Zaposlenici.FirstOrDefault(z => z.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            // Join RadnaMjesta and Odjeli to create the combined Radno Mjesto + Odjel
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .Join(
                    _context.Odjeli,
                    rm => rm.OdjelId,
                    o => o.Id,
                    (rm, o) => new
                    {
                        RadnoMjestoId = rm.Id,
                        CombinedName = $"{rm.Naziv} ({o.Naziv})"
                    }
                ).ToList();

            // Store the combined RadnoMjesto + Odjel in ViewBag for the dropdown
            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "RadnoMjestoId", "CombinedName", zaposlenik.RadnoMjestoId);

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
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdown in case of validation failure
            var radnaMjestaWithOdjeli = _context.RadnaMjesta
                .Join(
                    _context.Odjeli,
                    rm => rm.OdjelId,
                    o => o.Id,
                    (rm, o) => new
                    {
                        RadnoMjestoId = rm.Id,
                        CombinedName = $"{rm.Naziv} ({o.Naziv})"
                    }
                ).ToList();

            ViewBag.RadnaMjesta = new SelectList(radnaMjestaWithOdjeli, "RadnoMjestoId", "CombinedName", zaposlenik.RadnoMjestoId);

            return View(zaposlenik);
        }


        // GET: Zaposlenici/Delete/5
        public ActionResult Delete(int id)
        {
            Zaposlenik? zap = _context.Zaposlenici.FirstOrDefault(zap => zap.Id == id);
            if (zap != null && zap.RadnoMjestoId != null) ViewBag.RadnoMjesto = _context.RadnaMjesta.FirstOrDefault(rm => rm.Id == zap.RadnoMjestoId).Naziv;
            else ViewBag.RadnoMjesto = "Nema radno mjesto";
            if (zap != null) return View(zap);
            else
                return NotFound();
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
