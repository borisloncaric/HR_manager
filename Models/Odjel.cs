using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("odjeli")]
    public class Odjel
    {
        private int id;
        private string naziv;

        [Key]
        [Column("id")]
        [Display(Name = "Šifra odjela")]
        public int Id
        {
            get => this.id;
            set => this.id = value;  
        }

        [Column("naziv")]
        [Display(Name = "Naziv")]
        [Required(ErrorMessage ="{0} je obavezan")]
        [MinLength(2, ErrorMessage ="Unesite barem 2 znaka za {0}")]
        [MaxLength(255,ErrorMessage ="{0} može biti dužine najviše 255 znakova")]
        public string Naziv
        {
            get => this.naziv;
            set => this.naziv = value;  
        }


  


    }
}
