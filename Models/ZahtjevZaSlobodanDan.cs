namespace HR_menager.Models
{
    public class ZahtjevZaSlobodanDan
    {
       
        private int id;
        private Zaposlenik zaposlenik;
        private DateOnly datumZahtjeva;
        private DateOnly pocetniDatum;
        private DateOnly krajnjiDatum;
        private string razlog;
        private StatusZahtjeva status;

        public int Id { get => this.id; private set { this.id = value; } }
        public Zaposlenik Zaposlenik { get => this.zaposlenik;  set { this.zaposlenik = value; } }
        public DateOnly DatumZahtjeva { get => this.datumZahtjeva;  set { this.datumZahtjeva = value; } }
        public DateOnly PocetniDatum { get => this.pocetniDatum;  set { this.pocetniDatum = value; } }
        public DateOnly KrajnjiDatum { get => this.krajnjiDatum;  set { this.krajnjiDatum = value; } }
        public string Razlog { get => this.razlog;  set { this.razlog = value; } }
        public StatusZahtjeva StatusZahtjeva { get => this.status;  set { this.status = value; } }

        public ZahtjevZaSlobodanDan()
        {
         
        }

        public ZahtjevZaSlobodanDan(int id, Zaposlenik zap, DateOnly datumZahtjeva, DateOnly pocetniDatum, DateOnly krajnjiDatum, string razlog, StatusZahtjeva status)
        {
            this.Id = id;
            this.Zaposlenik = zap;
            this.DatumZahtjeva = datumZahtjeva;
            this.StatusZahtjeva = status;
            this.PocetniDatum = pocetniDatum;
            this.KrajnjiDatum = krajnjiDatum;
            this.Razlog = razlog;
        }

     
    }
}
