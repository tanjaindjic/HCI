using Microsoft.Win32;
using Projekat.Model;
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

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for izmeniTip.xaml
    /// </summary>
    public partial class izmeniTip : INotifyPropertyChanged
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
        private string oznaka;
        public string Oznaka
        {
            get
            {
                return oznaka;
            }
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
            get
            {
                return naziv;
            }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
        private string slika = null;

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

        private string opis;
        


        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        private Tip selektovan;
        public izmeniTip(Tip m, string k)
        {
            baza = new BazaPodataka(k);
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            selektovan = m;
            Oznaka = m.Oznaka;
            Opis = m.Opis;
            Naziv = m.Naziv;
            Slika = m.Slika;
            ikonica.Source = new BitmapImage(new Uri(m.Slika));
        }

        private void odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int idx = -1;
        public Tip izmenjen;
        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            izmenjen = new Tip(oznaka,naziv,opis,slika);
            
            baza.ucitajEtikete();
            idx = 0;
            foreach (Tip man in baza.Tipovi)
            {
                if (man.Oznaka == izmenjen.Oznaka)
                    break;
                idx++;
            }
            baza.Tipovi[idx] = izmenjen;
            baza.sacuvajTip();


            System.Windows.MessageBox.Show("Uspešna izmena tipa!", "Izmena tipa");
            this.Close();

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
                slika = fileDialog.FileName;
            }
        }
    }
}
