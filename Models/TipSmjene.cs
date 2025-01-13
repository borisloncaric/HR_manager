using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("tip_smjene")]
    public class TipSmjene
    {
        private int id;
        private string smjena;


        [Column("id")]
        [Display(Name = "ID smjene")]
        public int Id { get { return this.id; } set { this.id = value; } }

        [Column("smjena")]
        [Display(Name ="Smjena")]
        [Required(ErrorMessage ="Tip smjene je obavezan")]
        public string Smjena {  get { return this.smjena; } set { this.smjena = value; } }
    }














}
  

