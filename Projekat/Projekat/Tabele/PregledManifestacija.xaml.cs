using System;
using System.Collections.Generic;
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
using Projekat.Model;
using Projekat.Dijalozi;
using System.Collections.ObjectModel;

namespace Projekat.Tabele
{
    /// <summary>
    /// Interaction logic for PregledManifestacija.xaml
    /// </summary>
    public partial class PregledManifestacija : Window
    {
        private BazaPodataka baza;
        private ObservableCollection<Manifestacija> manif;
        string[] positive = { "MOŽE SE DONETI ALKOHOL", "MOŽE SE KUPITI ALKOHOL", "BESPLATNO", "NISKE CENE", "SREDNJE CENE", "VISOKE CENE", "DA", "NA OTVORENOM", "MLADI", "SREDOVEČNI" };
        string[] negative = { "NE", "NEMA ALKOHOLA", "STARIJI" };



        public ObservableCollection<Manifestacija> Manif
        {
            get { return manif; }
            set { manif = value; }
        }

        private string korisnik;
        public PregledManifestacija(string k)
        {
            korisnik = k;
            Manif = new ObservableCollection<Manifestacija>();
            baza = new BazaPodataka(k);
            baza.ucitajManifestacije();
            manif = baza.Manifestacije;
            this.DataContext = this;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


        }



        #region Click
       

        public int idx = -1;
        public Manifestacija izmenjena;
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedValue is Manifestacija)
            {
                Manifestacija m = (Manifestacija)dgrMain.SelectedValue;


                var s = new izmeniManifestaciju(m, korisnik);
                s.ShowDialog();
                if (s.idx != -1)
                {
                    baza.Manifestacije[s.idx] = s.Izmenjena;
                    baza.sacuvajManifestaciju();
                    baza.ucitajManifestacije();
                    Manif = baza.Manifestacije;
                    idx = s.idx;
                    izmenjena = s.Izmenjena;

                }
            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijednu manifestaciju.", "Izmena manifestacije");

            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            Manifestacija m = null;
            if (dgrMain.SelectedValue is Manifestacija)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete manifestaciju?", "Brisanje manifestacije", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        m = (Manifestacija)dgrMain.SelectedValue;
                        baza.brisanjeManifestacije(m);

                        Manif = baza.Manifestacije;
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }


            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali manifestaciju za brisanje!", "Brisanje manifestacije");

            }
        }

        #endregion Click

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(manif);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Manifestacija man = o as Manifestacija;
                    string[] words = filter.Split(' ');
                    if (words.Contains(""))
                        words = words.Where(word => word != "").ToArray();
                    return words.Any(word => man.Oznaka.ToUpper().Contains(word.ToUpper()) );
                };

                dgrMain.ItemsSource = manif;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(manif);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Manifestacija man = o as Manifestacija;
                    string[] words = filter.Split(' ');
                    if (words.Contains(""))
                        words = words.Where(word => word != "").ToArray();
                    return words.Any(word => man.Naziv.ToUpper().Contains(word.ToUpper()) );
                };

                dgrMain.ItemsSource = manif;
            }
        }
    }
}