namespace HR_menager.Models
{
    public class RasporedRadnogVremena
    {
        private int id;
        private Zaposlenik zaposlenik;
        private DateOnly datum;
        private int pocetakRada;
        private int zavrsetakRada;
        private int ukupniSati;
        private TipSmjene smjena;

        // Public properties with encapsulation
        public int Id { get; private set; }
        public Zaposlenik Zaposlenik { get; private set; }
        public DateOnly Datum { get; private set; }
        public int PocetakRada { get; private set; }
        public int ZavrsetakRada { get; private set; }
        public int UkupniSati { get; private set; }
        public TipSmjene Smjena { get; private set; }

        // Parameterless constructor
        public RasporedRadnogVremena()
        {
            this.id = 0;
            this.zaposlenik = new Zaposlenik();
            this.datum = DateOnly.FromDateTime(DateTime.Now);
            this.pocetakRada = 0;
            this.zavrsetakRada = 24;
            this.ukupniSati = 0;
            this.smjena = TipSmjene.Jutarnja;
        }

        // Parameterized constructor
        public RasporedRadnogVremena(int id, Zaposlenik zap, DateOnly datum, int pocetak, int kraj, int ukupniSati, TipSmjene smjena)
        {
            this.id = id;
            this.zaposlenik = zap;
            this.datum = datum;
            this.pocetakRada = pocetak;
            this.zavrsetakRada = kraj;
            this.ukupniSati = ukupniSati;
            this.smjena = smjena;
        }

        // Setter methods (only if you need to modify properties)
        public void PostaviId(int id) => this.id = id;
        public void PostaviZaposlenika(Zaposlenik zap) => this.zaposlenik = zap;
        public void PostaviDatum(DateOnly dat) => this.datum = dat;
        public void PostaviPocetakRada(int poc) => this.pocetakRada = poc;
        public void PostaviZavrsetakRada(int zav) => this.zavrsetakRada = zav;
        public void PostaviUkupneSate(int sati) => this.ukupniSati = sati;
        public void PostaviTipSmjene(TipSmjene tip) => this.smjena = tip;
    }
}
