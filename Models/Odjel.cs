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
        [Display(Name = "ID")]
        public int Id
        {
            get => this.id;
            set => this.id = value;  
        }

        [Column("naziv")]
        [Display(Name = "Naziv")]
        public string Naziv
        {
            get => this.naziv;
            set => this.naziv = value;  
        }


  


    }
}
