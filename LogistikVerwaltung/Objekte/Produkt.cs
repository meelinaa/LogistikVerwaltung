using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistikVerwaltung.Objekte
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public Lagerort Lagerort { get; set; }
        public Logistiker Lieferant { get; set; }
    }
}
