using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_menager.Models
{
    [Table("status_zahtjeva")]
    public class StatusZahtjeva
    {
        private int id;
        private string status;

        [Column("id")]
        [Display(Name = "Status ID")]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [Column("status")]
        [Display(Name = "Status zahtjeva")]
        [Required(ErrorMessage ="Tekst statusa je obavezan")]
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
