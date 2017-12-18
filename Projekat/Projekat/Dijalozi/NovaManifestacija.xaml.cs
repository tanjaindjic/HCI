using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Projekat.Model;
using System.ComponentModel;

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for NovaManifestacija.xaml
    /// </summary>
    public partial class NovaManifestacija : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private BazaPodataka baza;
        private string naziv;
        public string Naziv
        {
            get { return naziv; }
            set {
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
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }

        }
        private string oznaka;

        public string Oznaka
        {
            get { return oznaka; }
            set {
                if (value != oznaka)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        private string slika;

        public string Slika
        {
            get { return slika; }
            set {
                if (value != slika)
                {
                    slika = value;
                    OnPropertyChanged("Slika");
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



        private ObservableCollection<Etiketa> etikete;


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
    
        private ObservableCollection<Manifestacija> manifList;

        public ObservableCollection<Manifestacija> ManifList
        {
            get { return manifList; }
            set
            {
                if (manifList != value)
                {

                    manifList = value;
                    OnPropertyChanged("ManifList");
                }
            }
        }
        private string korisnik;
        public NovaManifestacija(string k)
        {
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
            baza = new BazaPodataka(k); 
            tip = new Tip();
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;

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
                slika = ikonica.Source.ToString();
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
                if (temp!= null && temp.Count > 0)
                {

                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    ObservableCollection<Etiketa> pomocna2 = etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    int idx = -1;
                    for (int i = 0; i < temp.Count; i++)
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
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (naziv == "" || oznaka == "" || tip_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste uneli neophodne informacije!", "Dodavanje manifestacije");
                return;
            }


            DateTime d = datum;
            bool invalidi = false, pusenje = false;
            if (invalidiDa.IsChecked == true)
            {
                invalidi = true;
            }

            if (pusenjeDa.IsChecked == true)
            {
                pusenje = true;
            }
            String alkohol = "";
            if (alkohol_comboBox.SelectedIndex.Equals(-1))
                alkohol = "";

            else if (alkohol_comboBox.SelectedItem.Equals("Nema alkohola"))
            {
                int idx = Alkohol.IndexOf("Nema alkohola");
                alkohol = Alkohol[idx];
            }
            else if (alkohol_comboBox.SelectedItem.Equals("Nema alkohola"))
            {
                int idx = Alkohol.IndexOf("Može se doneti alkohol");
                alkohol = Alkohol[idx];
            }
            else
            {
                int idx = Alkohol.IndexOf("Može se kupiti alkohol");
                alkohol = Alkohol[idx];
            }

            String mesto = "";

            if (mesto_comboBox.SelectedIndex.Equals(-1))
                mesto = "";

            else if (mesto_comboBox.SelectedItem.Equals("Na otvorenom"))
            {
                int idx = Mesto.IndexOf("Na otvorenom");
                mesto = Mesto[idx];
            }
            else
            {
                int idx = Mesto.IndexOf("Na zatvorenom");
                mesto = Mesto[idx];
            }
            ObservableCollection<Etiketa> listaEtiketa = etikete;

            String cena = "";
            if (cene_comboBox.SelectedIndex.Equals(-1))
                cena = "";
            else if (cene_comboBox.SelectedItem.Equals("Besplatno"))
            {
                int idx = Cena.IndexOf("Besplatno");
                cena = Cena[idx];
            }
            else if (cene_comboBox.SelectedItem.Equals("Niske cene"))
            {
                int idx = Cena.IndexOf("Niske cene");
                cena = Cena[idx];
            }
            else if (cene_comboBox.SelectedItem.Equals("Srednje cene"))
            {
                int idx = Cena.IndexOf("Visoke cene");
                cena = Cena[idx];
            }
            else
            {
                int idx = Cena.IndexOf("Niske cene");
                cena = Cena[idx];
            }

            String publika = "";
            if (publika_comboBox.SelectedIndex.Equals(-1))
                publika = "";
            else if (publika_comboBox.SelectedItem.Equals("Mladi"))
            {
                int idx = Publika.IndexOf("Mladi");
                publika = Publika[idx];
            }
            else if (publika_comboBox.SelectedItem.Equals("Sredovečni"))
            {
                int idx = Publika.IndexOf("Sredovečni");
                publika = Publika[idx];
            }
            else
            {
                int idx = Publika.IndexOf("Stariji");
                publika = Publika[idx];
            }

            Opis = opis_textBox.Text;
            if (slika == null && tip.Slika != null)
            {
                if (tip.Slika != "")
                    slika = tip.Slika;
            }
            else if (slika == null && tip.Slika == null)
            {
                slika = System.IO.Path.GetFullPath(@"..\..\") + "Images\\defLoc.png";
            }
            string s = oznaka;
            s = s.Replace(' ', '_');
            oznaka = s;

            datum = (DateTime)datumPicker.SelectedDate;
            Manifestacija m = new Manifestacija(oznaka, naziv, opis, tip, alkohol, invalidi, pusenje, cena, datum, etikete, slika, mesto, publika);
            bool passed = baza.novaManifestacija(m);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno ste dodali novu manifestaciju.", "Dodavanje manifestacije");
                baza.sacuvajManifestaciju();
                ManifList = baza.Manifestacije;
                this.Close();

            }
            else
                System.Windows.MessageBox.Show("Vec postoji manifestacija sa tom oznakom!", "Dodavanje manifestacije");
        }

        private void cena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}