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
        public int Id { get; set; }
        public Zaposlenik Zaposlenik { get; set; }
        public DateOnly Datum { get; set; }
        public int PocetakRada { get; set; }
        public int ZavrsetakRada { get; set; }
        public int UkupniSati { get; set; }
        public TipSmjene Smjena { get; set; }

     

    }
}
