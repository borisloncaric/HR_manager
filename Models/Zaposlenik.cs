using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("zaposlenici")]
    public class Zaposlenik
    {
        private int id;
        private string ime;
        private string prezime;
        private string brojTelefona;
        private int radnoMjestoId;

        [Key]
        [Column("id")]
        [Display(Name = "ID")]
        public int Id { get => this.id;  set { this.id = value; } }


        [Column("ime")]
        [Display(Name ="Ime")]
        public string Ime { get => this.ime;  set { this.ime = value; } }
        [Column("prezime")]
        [Display(Name = "Prezime")]


        public string Prezime { get => this.prezime; set { this.prezime = value; } }
        [Column("broj_telefona")]
        [Display(Name = "Kontakt broj")]
        public string BrojTelefona { get => this.brojTelefona;  set { this.brojTelefona = value; } }


        [ForeignKey("radno_mjesto")]
        [Column("radno_mjesto")]
        [Display(Name = "Radno mjesto")]
        public int RadnoMjestoId { get => this.radnoMjestoId;  set { this.radnoMjestoId = value; } }

      

     
    }
}
