using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LogistikVerwaltung.Frontend
{
    public class LagerLogikFE
    {

        public void LagerLöschen(ListBox lagerorteListBox)
        {
            // schauen ob das Lager in der Datenbank existiert
            // wenn ja, dann löschen mit OberservableCollection
        }

        public void LagerSpeichern(TextBox lagerNameTB, TextBox lagerAdresseTB)
        {
            // schauen ob es das schon in der Datenbank gibt und mit Namen vergleichen statt ID
            // wenn ja, dann updaten
            // wenn nein, dann neu anlegen
        }

    }
}
