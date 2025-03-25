using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistikVerwaltung.Objekte {

    public class Lagerort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
    }

    public class Logistiker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kontakt { get; set; }
        public string Adresse { get; set; }
    }

    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public Lagerort Lagerort { get; set; }
        public Logistiker Lieferant { get; set; }
    }

    public class Lieferung
    {
        public int Id { get; set; }
        public Produkt Produkt { get; set; }
        public int Menge { get; set; }
        public DateTime Lieferdatum { get; set; }
    }


}