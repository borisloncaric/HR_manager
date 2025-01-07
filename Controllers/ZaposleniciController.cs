using HR_menager.BazePodataka_demo;
using HR_menager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            
            var zaposlenici = _context.Zaposlenici.ToList();
            var radnaMjesta = _context.RadnaMjesta.ToDictionary(r => r.Id, r => r.Naziv);
            ViewBag.RadnaMjesta = radnaMjesta;

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
            ViewBag.RadnaMjesta = _context.RadnaMjesta.ToList();
            return View();
        }

        // POST: Zaposlenici/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Ime, Prezime, BrojTelefona, RadnoMjestoId")] Zaposlenik zaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Zaposlenici.Add(zaposlenik);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }

            ViewBag.RadnaMjesta = _context.RadnaMjesta.ToList();
            return View(zaposlenik);
        }

        // GET: Zaposlenici/Edit/5
        public ActionResult Edit(int id)
        {
            var zap = _context.Zaposlenici
       .FirstOrDefault(z => z.Id == id);

            if (zap == null)
            {
                return NotFound();
            }

            ViewBag.RadnaMjesta = _context.RadnaMjesta.ToList();

            return View(zap);
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

            if (ModelState.IsValid)
            {
                try
                {
                    var radnoMjesto = _context.RadnaMjesta.FirstOrDefault(r => r.Id == zaposlenik.RadnoMjestoId);
                    if (radnoMjesto != null)
                    {
                        zaposlenik.RadnoMjestoId = radnoMjesto.Id; 
                    }

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

            ViewBag.RadnaMjesta = _context.RadnaMjesta.ToList();
            return View(zaposlenik);
        }


        // GET: Zaposlenici/Delete/5
        public ActionResult Delete(int id)
        {
            Zaposlenik? zap = _context.Zaposlenici.FirstOrDefault(zap => zap.Id == id);
            ViewBag.radnoMjesto = _context.RadnaMjesta.FirstOrDefault(rm=>rm.Id == zap.RadnoMjestoId).Naziv;
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
