using Microsoft.Win32;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for izmeniManifestaciju.xaml
    /// </summary>
    public partial class izmeniManifestaciju : INotifyPropertyChanged
    {
        
    
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private string oznaka;

        public string Oznaka
        {
            get { return oznaka; }
            set
            {
                if (value != oznaka)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        private Tip tip;

        public Tip Tip
        {
            get { return tip; }
            set {
                if (value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        private string opis;

        public string Opis
        {
            get { return opis; }
            set{
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }


        private string oznakaTipa;

        public string OznakaTipa
        {
            get { return oznakaTipa; }
            set
            {
                if (value != oznakaTipa)
                {
                    oznakaTipa = value;
                    OnPropertyChanged("OznakaTipa");
                }
            }
        }

        private string sAlkohol;
        public string SAlkohol
        {
            get { return sAlkohol; }
            set
            {
                if (value != sAlkohol)
                {
                    sAlkohol = value;
                    OnPropertyChanged("SAlkohol");
                }
            }
        }

        private string sMesto;
        public string SMesto
        {
            get { return sMesto; }
            set
            {
                if (value != sMesto)
                {
                    sMesto = value;
                    OnPropertyChanged("SMesto");
                }
            }
        }

        private string sCena;
        public string SCena
        {
            get { return sCena; }
            set
            {
                if (value != sCena)
                {
                    sCena = value;
                    OnPropertyChanged("SCena");
                }
            }
        }
        private string sPublika;
        public string SPublika
        {
            get { return sPublika; }
            set
            {
                if (value != sPublika)
                {
                    sPublika = value;
                    OnPropertyChanged("SPublika");
                }
            }
        }

        private bool bPusenjeDa;
        public bool BPusenjeDa
        {
            get { return bPusenjeDa; }
            set
            {
                if (value != bPusenjeDa)
                {
                    bPusenjeDa = value;
                    OnPropertyChanged("BPusenjeDa");
                }
            }
        }
        private bool bPusenjeNe;
        public bool BPusenjeNe
        {
            get { return bPusenjeNe; }
            set
            {
                if (value != bPusenjeNe)
                {
                    bPusenjeNe = value;
                    OnPropertyChanged("BPusenjeNe");
                }
            }
        }


        private bool bInvalidiDa;
        public bool BInvalidiDa
        {
            get { return bInvalidiDa; }
            set
            {
                if (value != bInvalidiDa)
                {
                    bInvalidiDa = value;
                    OnPropertyChanged("BInvalidiDa");
                }
            }

        }

        private bool bInvalidiNe;
        public bool BInvalidiNe
        {
            get { return bInvalidiNe; }
            set
            {
                if (value != bInvalidiNe)
                {
                    bInvalidiNe = value;
                    OnPropertyChanged("BInvalidiNe");
                }
            }

        }
        private DateTime datum;
        public DateTime Datum
        {
            get { return datum; }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }
        private string slika;
        public string Slika
        {
            get { return slika; }
            set
            {            
                    if (value != slika)
                    {
                        slika = value;
                        OnPropertyChanged("Slika");
                    }
                
            }
        }
        private string listaEtiketa;
        public string ListaEtiketa
        {
            get { return listaEtiketa; }
            set
            {
                if (value != listaEtiketa)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }

        private ObservableCollection<Etiketa> etikete;
        
        public ObservableCollection<Etiketa>  Etikete
        {
            get { return etikete; }
            set { etikete = value; }
        }
        private BazaPodataka baza;
        private string korisnik;
        private Manifestacija selektovana;
        public izmeniManifestaciju(Manifestacija m, string k)
        {
            baza = new BazaPodataka(k);
            korisnik = k;
            Alkohol = new ObservableCollection<string>();
            Alkohol.Add("Nema alkohola");
            Alkohol.Add("Može se doneti alkohol");
            Alkohol.Add("Može se kupiti alkohol");

            Mesto = new ObservableCollection<string>();
            Mesto.Add("Na otvorenom");
            Mesto.Add("Na zatvorenom");

            Publika = new ObservableCollection<string>();
            Publika.Add("Mladi");
            Publika.Add("Sredovečni");
            Publika.Add("Stariji");

            Cena = new ObservableCollection<string>();
            Cena.Add("Besplatno");
            Cena.Add("Niske cene");
            Cena.Add("Srednje cene");
            Cena.Add("Visoke cene");

            etikete = new ObservableCollection<Etiketa>();

            tip = new Tip();
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            selektovana = m;
            Naziv = m.Naziv;
            Oznaka = m.Oznaka;
            Tip = m.Tip;
            OznakaTipa = Tip.Oznaka;
            Opis = m.Opis;
            SAlkohol = m.Alkohol;
            SMesto = m.Mesto;
            SCena = m.Cena;
            SPublika = m.Publika;
            Datum = m.Datum;
            bool bPusenje = m.Pusenje;
            if (bPusenje)
            {
                BPusenjeDa = true;
                BPusenjeNe = false;
            }
            else
            {
                BPusenjeDa = false;
                BPusenjeNe = true;
            }

            bool bInvalidi = m.Invalidi;
            if (bInvalidi)
            {
                BInvalidiDa = true;
                BInvalidiNe = false;
            }
            else
            {
                BInvalidiDa = false;
                BInvalidiNe = true;
            }
            Datum = m.Datum;
            Slika = m.Slika;
            Etikete = m.Etikete;
            if (Etikete.Count > 0)
            {
                StringBuilder sb = new StringBuilder(ListaEtiketa);
                foreach(Etiketa et in Etikete)
                {
                    sb.Append(et.Oznaka);
                    sb.Append(System.Environment.NewLine);
                }
                
                ListaEtiketa = sb.ToString();
                
            }
           
            
        }

        public ObservableCollection<string> Alkohol
        {
            get;
            set;
        }

        public ObservableCollection<string> Mesto
        {
            get;
            set;
        }

        public ObservableCollection<string> Publika
        {
            get;
            set;
        }
        public ObservableCollection<string> Cena
        {
            get;
            set;
        }


        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void izaberiIkonicu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            fileDialog.Title = "Izaberi ikonicu";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ikonica.Source = new BitmapImage(new Uri(fileDialog.FileName));
                Slika = ikonica.Source.ToString();
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void izaberiTip_Click(object sender, RoutedEventArgs e)
        {
            odabirTipa s = new odabirTipa(korisnik);
            s.ShowDialog();

            Tip temp = s.Odabran;
            if (temp != null)
            {
                tip = temp;
                OznakaTipa = tip.Oznaka;
                tip_textBox.Text = OznakaTipa;
            }

        }


        private void izaberiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            odabirEtikete s = new odabirEtikete(korisnik);
            s.ShowDialog();
            ObservableCollection<Etiketa> temp = s.Odabrana;
            if (s.dodavanje)
            {
                ObservableCollection<Etiketa> pomocna = new ObservableCollection<Etiketa>();
                if (temp != null)
                {
                    bool b = false;
                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);

                    foreach (Etiketa et in etikete)
                    {
                        foreach (Etiketa et2 in temp)
                        {
                            if (et.Oznaka.Equals(et2.Oznaka))
                            {
                                b = true;
                            }
                            else b = false;
                            if (b)
                            {
                                pomocna.Add(et2);

                                sb.Append(et2.Oznaka);
                                sb.Append(System.Environment.NewLine);
                            }

                        }
                    }
                    etikete = pomocna;
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
            }
            else
            {

                if (temp!=null && temp.Count > 0)
                {
                    
                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    ObservableCollection<Etiketa> pomocna2 = etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    int idx = -1;
                    for(int i = 0; i< temp.Count; i++)
                    {
                        idx = 0;
                        foreach (Etiketa et in etikete)
                        {
                            if (et.Oznaka.Equals(temp.ElementAt(i).Oznaka))
                                break;
                            idx++;
                        }
                        if (idx != -1)
                        {
                            etikete.RemoveAt(idx);
                        }
                    }
                    foreach (Etiketa et in temp)
                    {
                        etikete.Remove(et);
                    }

                    foreach (Etiketa et in etikete)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
            }
            
        }

        private void button3_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void alkohol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_Copy4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tip_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void invalidiDa_Checked(object sender, RoutedEventArgs e)
        {

        }

        private Manifestacija izmenjena;
        public Manifestacija Izmenjena
        {
            get { return izmenjena; }
            set { izmenjena = value; }
        }
        public int idx = -1;
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (naziv_textbox.Text == "" || oznaka_textbox.Text == "" || tip_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste popunili obavezna polja!", "Izmena manifestacije");
            }
            else { 
                bool bInvalidi = false;
                if (BInvalidiDa)
                    bInvalidi = true;
                bool bPusenje = false;
                if (BPusenjeDa)
                    bPusenje = true;
                Izmenjena = new Manifestacija(Oznaka, Naziv, Opis, Tip, SAlkohol, bInvalidi, bPusenje, SCena, Datum, etikete, Slika, SMesto, SPublika);
                BazaPodataka b = new BazaPodataka(korisnik);
                idx = 0;
                foreach (Manifestacija man in b.Manifestacije)
                {
                    if (man.Oznaka == izmenjena.Oznaka)
                        break;
                    idx++;
                }
                izmenjena.X = selektovana.X;
                izmenjena.Y = selektovana.Y;
                b.Manifestacije.RemoveAt(idx);
                b.Manifestacije.Add(Izmenjena);
                b.sacuvajManifestaciju();
               
                System.Windows.MessageBox.Show("Uspešna izmena manifestacije!", "Izmena manifestacije");
                this.Close();
            }
        }

        private void opis_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


