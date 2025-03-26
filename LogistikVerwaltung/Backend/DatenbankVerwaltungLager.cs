using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistikVerwaltung;

namespace LogistikVerwaltung.Backend
{
    public class DatenbankVerwaltungLager
    {
        public static void NeuesLagerErstellen(string name, string adresse)
        {
            using var connection = Datenbank.GetConnection();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO lager (name, adresse)
                VALUES (@name, @adresse";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@adresse", adresse);

            cmd.ExecuteNonQuery();
        }

        /*
         * HIER MUSS ICH DIE OBJEKTE VERWENDEN 
        public static Produkt GetLagerById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM lager WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Lagerort
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Adresse = reader.GetString(2)
                };
            }

            return null;
        }
        */
    }

}
