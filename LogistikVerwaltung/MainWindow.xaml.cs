﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogistikVerwaltung.Frontend;
using LogistikVerwaltung.Backend;
using System.Collections.ObjectModel;
using System.Reflection;


namespace LogistikVerwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            Datenbank.InitialisiereTabellen();
            AlleListBoxenAktivieren();
            this.DataContext = this;
            InitializeComponent();
        }


        // Observable Collections für die Listboxen Lager
        private ObservableCollection<Lager> lagerListe = new ObservableCollection<Lager>();

        public ObservableCollection<Lager> LagerListe
        {
            get { return lagerListe; }
            set { lagerListe = value; }
        }

        // Observable Collections für die Listboxen Lieferanten
        private ObservableCollection<Lieferant> lieferantenListe = new ObservableCollection<Lieferant>();

        public ObservableCollection<Lieferant> LieferantenListe
        {
            get { return lieferantenListe; }
            set { lieferantenListe = value; }
        }

        // Observable Collections für die Listboxen Produkte
        private ObservableCollection<Produkt> produkteListe = new ObservableCollection<Produkt>();

        public ObservableCollection<Produkt> ProdukteListe
        {
            get { return produkteListe; }
            set { produkteListe = value; }
        }

        // Observable Collections für die Listboxen Produkte
        private ObservableCollection<Lieferung> lieferungenListe = new ObservableCollection<Lieferung>();

        public ObservableCollection<Lieferung> LieferungenListe
        {
            get { return lieferungenListe; }
            set { lieferungenListe = value; }
        }

        // Speichern der eingegebenen Daten
        private void OnSpeichernClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "LagerSpeichernBTN":
                        LagerLogikFE lagerLogikFE = new();
                        lagerLogikFE.LagerSpeichern(this.LagerNameTB, this.LagerAdresseTB);
                        this.LagerNameTB.Text = "";
                        this.LagerAdresseTB.Text = "";
                        LagerorteListBox_SelectionChanged(sender , null);
                        break;
                    case "LieferantSpeichernBTN":
                        LieferantenLogikFE lieferantenLogikFE = new();
                        lieferantenLogikFE.LieferantSpeichern(this.LieferantNameTB, this.LieferantEmailTB, this.LieferantOrtTB);
                        break;
                    case "ProduktSpeichernBTN":
                        ProdukteLogikFE produkteLogikFE = new();
                        produkteLogikFE.ProduktSpeichern(this.ProduktNameTB, this.ProduktBeschreibungTB, this.ProduktPreisTB, this.ProduktLagerortCB, this.ProduktLieferantCB);
                        break;
                    case "LieferungSpeichernBTN":
                        LieferLogikFE lieferLogikFE = new();
                        lieferLogikFE.LieferungSpeichern(this.LieferungProdukteCB, this.LieferungMengeTB, this.LieferungLieferantCB, this.LieferungDatumDP);
                        break;
                }
            }
        }

        // Löschen der ausgewählten Elemente
        private void Löschen_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "LagerLöschen":
                        LagerLogikFE lagerLogikFE = new();
                        lagerLogikFE.LagerLöschen(this.LagerorteListBox);
                        break;
                    case "LieferantLöschen":
                        LieferantenLogikFE lieferantenLogikFE = new();
                        lieferantenLogikFE.LieferantLöschen(this.LieferantenListBox);
                        break;
                    case "ProduktLöschen":
                        ProdukteLogikFE produkteLogikFE = new();
                        produkteLogikFE.ProduktLöschen(this.ProdukteListBox);
                        break;
                    case "LieferungLöschen":
                        LieferLogikFE lieferLogikFE = new();
                        lieferLogikFE.LieferungLöschen(this.LieferungenListBox);
                        break;
                }
            }
        }

        // Löschen aller Elemente in den jeweiligen Datenbanken/Listboxen
        private void AllesLöschenBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "AllesLöschenLagerBTN":
                        LagerLogikFE lagerLogikFE = new();
                        lagerLogikFE.AllesLöschen();
                        LagerorteListBox_SelectionChanged(sender, null);
                        break;
                    case "AllesLöschenLieferantenBTN":
                        LieferantenLogikFE lieferantenLogikFE = new();
                        lieferantenLogikFE.LieferantLöschen(this.LieferantenListBox);
                        break;
                    case "AllesLöschenProdukteBTN":
                        ProdukteLogikFE produkteLogikFE = new();
                        produkteLogikFE.ProduktLöschen(this.ProdukteListBox);
                        break;
                    case "AllesLöschenLieferungenBTN":
                        LieferLogikFE lieferLogikFE = new();
                        lieferLogikFE.LieferungLöschen(this.LieferungenListBox);
                        break;
                }
            }
        }

        // Inhalt der Listbox werden in die Inputfelder geschrieben
        private void Bearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "LagerBearbeitenBTN":
                        LagerBearbeiten();
                        break;
                    case "LieferantBearbeitenBTN":
                        LieferantBearbeiten();
                        break;
                    case "ProduktBearbeitenBTN":
                        ProduktBearbeiten();
                        break;
                    case "LieferungBearbeitenBTN":
                        LieferungBearbeiten();
                        break;
                }
            }
        }

        private void LieferungBearbeiten()
        {
            Lieferung lieferung = (Lieferung)this.LieferungenListBox.SelectedItem;
            this.LieferungProdukteCB.Text = lieferung.Produkt.Name;
            this.LieferungMengeTB.Text = lieferung.Menge.ToString();
            this.LieferungLieferantCB.Text = lieferung.Produkt.Lieferant.Name;
            this.LieferungDatumDP.Text = lieferung.Lieferdatum.ToString();
        }

        private void ProduktBearbeiten()
        {
            Produkt produkt = (Produkt)this.ProdukteListBox.SelectedItem;
            this.ProduktNameTB.Text = produkt.Name;
            this.ProduktBeschreibungTB.Text = produkt.Beschreibung;
            this.ProduktPreisTB.Text = produkt.Preis.ToString();
            this.ProduktLagerortCB.Text = produkt.Lagerort.Name;
            this.ProduktLieferantCB.Text = produkt.Lieferant.Name;
        }

        private void LieferantBearbeiten()
        {
            Lieferant lieferant = (Lieferant)this.LieferantenListBox.SelectedItem;
            this.LieferantNameTB.Text = lieferant.Name;
            this.LieferantEmailTB.Text = lieferant.Kontakt;
            this.LieferantOrtTB.Text = lieferant.Adresse;
        }

        private void LagerBearbeiten()
        {
            Lager lager = (Lager)this.LagerorteListBox.SelectedItem;
            this.LagerNameTB.Text = lager.Name;
            this.LagerAdresseTB.Text = lager.Adresse;
        }

        // Alle Listboxen werden aktiviert
        private void AlleListBoxenAktivieren()
        {
            LagerorteListBox_SelectionChanged(null, null);
            LieferantenListBox_SelectionChanged(null, null);
            ProdukteListBox_SelectionChanged(null, null);
            LieferungenListBox_SelectionChanged(null, null);
        }

        // Synkronisierung der Listboxen mit den Datenbanken
        private void LagerorteListBox_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            LagerListe.Clear();
           

            var connection = Datenbank.GetConnection();

            var lagerListeCmd = connection.CreateCommand();
            lagerListeCmd.CommandText = "SELECT * FROM lager";

            var reader = lagerListeCmd.ExecuteReader();
            
            while (reader.Read())
            {
                Lager lager = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                LagerListe.Add(lager);
            }
            
        }

        private void LieferantenListBox_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {

        }

        private void ProdukteDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {

        }

        private void ProdukteListBox_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {

        }

        private void LieferungenListBox_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {

        }

        
    }

    // Objekte für observable collections
    public class Lager(int id, string name, string adresse)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Adresse { get; set; } = adresse;
    }

    public class Lieferant(int id, string name, string kontakt, string adresse)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Kontakt { get; set; } = kontakt;
        public string Adresse { get; set; } = adresse;
    }

    public class Produkt(int id, string name, string beschreibung,int preis, Lager lagerort, Lieferant lieferant)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Beschreibung { get; set; } = beschreibung;
        public int Preis { get; set; } = preis;
        public Lager Lagerort { get; set; } = lagerort;
        public Lieferant Lieferant { get; set; } = lieferant;
    }

    public class Lieferung(int id, Produkt produkt, int menge, DateTime lieferdatum)
    {
        public int Id { get; set; } = id;
        public Produkt Produkt { get; set; } = produkt;
        public int Menge { get; set; } = menge;
        public DateTime Lieferdatum { get; set; } = lieferdatum;
    }
}