using HR_menager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.BazePodataka_demo
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        
        public DbSet<Zaposlenik> Zaposlenici { get; set; }
        public DbSet<RadnoMjesto> RadnaMjesta { get; set; }
        public DbSet<Odjel> Odjeli { get; set; }

        //zahtjev za slobodan dan
        public DbSet<Zahtjev> Zahtjevi { get; set; }

        //statusi
        public DbSet<StatusZahtjeva> StatusiZahtjeva { get; set; }

    }
}
