using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistikVerwaltung.Backend
{
    public static class Datenbank
    {
        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection("Data Source=mydata.db");
            connection.Open();
            return connection;
        }

        public static void InitialisiereTabellen()
        {
            using var connection = GetConnection();

            // Lager
            var lagerCmd = connection.CreateCommand();
            lagerCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS lager (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    adresse TEXT
                )";
            lagerCmd.ExecuteNonQuery();

            // Lieferant
            var lieferantCmd = connection.CreateCommand();
            lieferantCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS lieferant (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    kontakt TEXT,
                    adresse TEXT
                )";
            lieferantCmd.ExecuteNonQuery();

            // Produkt
            var produktCmd = connection.CreateCommand();
            produktCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS produkt (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    beschreibung TEXT,
                    lagerort_id INTEGER,
                    lieferant_id INTEGER,
                    FOREIGN KEY (lagerort_id) REFERENCES lager(id),
                    FOREIGN KEY (lieferant_id) REFERENCES lieferant(id)
                )";
            produktCmd.ExecuteNonQuery();

            // Lieferung
            var lieferungCmd = connection.CreateCommand();
            lieferungCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS lieferung (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    produkt_id INTEGER,
                    menge INTEGER,
                    datum TEXT,
                    lieferant_id INTEGER,
                    FOREIGN KEY (produkt_id) REFERENCES produkt(id),
                    FOREIGN KEY (lieferant_id) REFERENCES lieferant(id)
                )";
            lieferungCmd.ExecuteNonQuery();
        }
    }
}
