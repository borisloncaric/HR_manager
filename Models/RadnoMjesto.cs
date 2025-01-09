using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("radna_mjesta")]
    public class RadnoMjesto
    {
        private int id;
        private string naziv;
        private int? odjelId;
        private Odjel? odjel;
        // Public properties with encapsulation
        [Key]
        [Column("id")]
        [Display(Name ="Šifra radnog mjesta")]
        public int Id { get => this.id; set { this.id = value; } }
        [Column("naziv")]
        [Display(Name ="Naziv")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [MinLength(2, ErrorMessage = "Unesite barem 2 znaka za {0}")]
        [MaxLength(255, ErrorMessage = "{0} može biti dužine najviše 255 znakova")]
        public string Naziv { get =>this.naziv; set { this.naziv = value; } }


        [ForeignKey("odjel")]
        [Column("odjel")]
        [Display(Name ="Odjel")]
        //[Range(1, int.MaxValue, ErrorMessage = "Odaberite odjel")]
        public int? OdjelId { get => this.odjelId; set { this.odjelId = value; } }

        public Odjel? Odjel { get => this.odjel; set { this.odjel = value; } }

     
    }
}
