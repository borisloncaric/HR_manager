using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("zahtjev_za_slobodan_dan")]
    public class Zahtjev
    {
       
        private int id;
        private Zaposlenik? podnositelj;
        private int podnositeljId;
        private DateOnly? datumZahtjeva;
        private DateOnly? pocetniDatum;
        private DateOnly? krajnjiDatum;
        private string razlog;
        private StatusZahtjeva? statusZahtjeva;
        private int? statusZahtjevaId;
        //osoba koja je obradila zahtjev
        private int? obradioZaposlenikId;
        private Zaposlenik? obradioZaposlenik;

        [Column("id")]
        [Display(Name = "Šifra zahtjeva")]
        public int Id { get => this.id; set { this.id = value; } }

        [ForeignKey("zaposlenik_id")]
        [Column("zaposlenik_id")]
        [Display(Name ="Podnositelj zahtjeva")]
        //[Required(ErrorMessage ="Podnositelj zahtjeva je obavezan")]
        public int PodnositeljId { get => this.podnositeljId;  set { this.podnositeljId = value; } }

        public Zaposlenik? Podnositelj { get => this.podnositelj; set { this.podnositelj = value; } }

        [Column("datum_zahtjeva")]
        [Display(Name ="Datum podnošenja")]
        [Required(ErrorMessage ="Datum podnošenja zahtjeva je obavezan")]
        public DateOnly? DatumZahtjeva { get => this.datumZahtjeva;  set { this.datumZahtjeva = value; } }


        [Column("pocetni_datum")]
        [Display(Name ="Početni datum")]
        [Required(ErrorMessage ="Početni datum je obavezan")]
        public DateOnly? PocetniDatum { get => this.pocetniDatum;  set { this.pocetniDatum = value; } }

        [Column("krajnji_datum")]
        [Display(Name ="Datum završetka")]
        [Required(ErrorMessage ="Datum završetka je obavezan")]
        public DateOnly? KrajnjiDatum { get => this.krajnjiDatum;  set { this.krajnjiDatum = value; } }

        [Column("razlog")]
        [Display(Name ="Razlog")]
        [Required(ErrorMessage ="Razlog je obavezan")]
        public string Razlog { get => this.razlog;  set { this.razlog = value; } }

        [ForeignKey("status_zahtjeva")]
        [Column("status_zahtjeva")]
        [Display(Name ="Status zahtjeva")]
        [Range( 1, Int32.MaxValue, ErrorMessage = "Izaberite status zahtjeva")]
        public int? StatusZahtjevaId { get => this.statusZahtjevaId;  set { this.statusZahtjevaId = value; } }

        public StatusZahtjeva? StatusZahtjeva { get => this.statusZahtjeva; set { this.statusZahtjeva = value; } }

        [ForeignKey("obradio_zaposlenik_id")]
        [Column("obradio_zaposlenik_id")]
       
        public int? ObradioZaposlenikId { get => this.obradioZaposlenikId; set { this.obradioZaposlenikId = value; } }

        [Display(Name ="Zaposlenik koji je obradio zahtjev")]
        public Zaposlenik? ObradioZaposlenik { get => this.obradioZaposlenik; set { this.obradioZaposlenik = value; } }
       

     
    }
}
