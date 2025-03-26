using LogistikVerwaltung.Backend;
using System;
using System.Collections.Generic;
using System.Data;
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

            var connection = Datenbank.GetConnection();

            var lagerCmd = connection.CreateCommand();
            lagerCmd.CommandText = @"
                DELETE FROM lager
                WHERE id = @id";
            lagerCmd.Parameters.AddWithValue("@id", lagerorteListBox.SelectedIndex);
            lagerCmd.ExecuteNonQuery();

        }

        public void LagerSpeichern(TextBox lagerNameTB, TextBox lagerAdresseTB)
        {
            // schauen ob es das schon in der Datenbank gibt und mit Namen vergleichen statt ID
            // wenn ja, dann updaten
            // wenn nein, dann neu anlegen

            var connection = Datenbank.GetConnection();

            // Checken ob Lager bereits existiert
            /*
             * Unsicher wie ich das logisch aufbauen soll, dass ich auch wenn Name und Adresse anders sind, trotzdem mit der ID arbeiten kann
             * evtl über OberservableCollection
             * 
            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = @"
                SELECT COUNT(*) 
                FROM lager 
                WHERE name = @name";
            checkCmd.Parameters.AddWithValue("@name", lagerNameTB.Text);

            var count = Convert.ToInt32(checkCmd.ExecuteScalar());

            */
            var count = 0; // löschen wenn ich das oben korrigiert hab. Nur damit "else" ausgeführt wird
            var lagerCmd = connection.CreateCommand();

            if (count > 0)
            {

                // Lager existert bereits
                lagerCmd.CommandText = @"
                UPDATE ";
            }
            else
            {
                // Lager wird neu angelegt
                lagerCmd.CommandText = @"
                INSERT INTO lager (name, adresse)
                VALUES (@name, @adresse)";
                lagerCmd.Parameters.AddWithValue("@name", lagerNameTB.Text);
                lagerCmd.Parameters.AddWithValue("@adresse", lagerAdresseTB.Text);

                lagerCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Lager erfolgreich gespeichert");




        }

        internal void AllesLöschen()
        {
            // Alle Lager löschen
            var connection = Datenbank.GetConnection();
            var lagerCmd = connection.CreateCommand();
            lagerCmd.CommandText = @"
                DELETE FROM lager";
            lagerCmd.ExecuteNonQuery();
        }
    }
}
