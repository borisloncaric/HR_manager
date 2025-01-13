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
        private int? radnoMjestoId;
        private RadnoMjesto? radno_mjesto;

        [Key]
        [Column("id")]
        [Display(Name = "ID")]
        public int Id { get => this.id;  set { this.id = value; } }


        [Column("ime")]
        [Display(Name ="Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [MinLength(2, ErrorMessage = "Unesite barem 2 znaka za {0}")]
        [MaxLength(255, ErrorMessage = "{0} može biti dužine najviše 255 znakova")]
        public string Ime { get => this.ime;  set { this.ime = value; } }


        [Column("prezime")]
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [MinLength(2, ErrorMessage = "Unesite barem 2 znaka za {0}")]
        [MaxLength(255, ErrorMessage = "{0} može biti dužine najviše 255 znakova")]
        public string Prezime { get => this.prezime; set { this.prezime = value; } }


        [Column("broj_telefona")]
        [Display(Name = "Kontakt broj")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [MinLength(6, ErrorMessage = "Unesite barem 6 znakova za {0}")]
        [MaxLength(15, ErrorMessage = "{0} može biti dužine najviše 15 znakova")]
        public string BrojTelefona { get => this.brojTelefona;  set { this.brojTelefona = value; } }


        [ForeignKey("radno_mjesto")]
        [Column("radno_mjesto")]
        [Display(Name = "Radno mjesto")]
        public int? RadnoMjestoId { get => this.radnoMjestoId;  set { this.radnoMjestoId = value; } }

        public RadnoMjesto? Radnomjesto { get => this.radno_mjesto; set { this.radno_mjesto = value; } }

        public string VratiImePrezime()
        {

            if (this.Ime != null && this.Ime != string.Empty) 
                return (this.ime + " " + this.prezime);
            else return "Ime i Prezime";
        }
     
    }
}
