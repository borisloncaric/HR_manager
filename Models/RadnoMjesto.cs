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
        private int odjelId;
        private Odjel? odjel;
        // Public properties with encapsulation
        [Key]
        [Column("id")]
        [Display(Name ="Šifra radnog mjesta")]
        public int Id { get => this.id; set { this.id = value; } }
        [Column("naziv")]
        [Display(Name ="Naziv")]
        public string Naziv { get =>this.naziv; set { this.naziv = value; } }
        [ForeignKey("odjel")]
        [Column("odjel")]
        [Display(Name ="Odjel")]
        public int OdjelId { get => this.odjelId; set { this.odjelId = value; } }

        public Odjel? Odjel { get => this.odjel; set { this.odjel = value; } }

     
    }
}
