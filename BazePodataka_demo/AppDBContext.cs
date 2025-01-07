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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=HR_Manager;user=root;password=",
                                    new MySqlServerVersion(new Version(8, 0, 23)));
        }
    }
}
