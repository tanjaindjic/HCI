using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NoviTip.xaml
    /// </summary>
    public partial class NoviTip : INotifyPropertyChanged
    {
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
            set { slika = value; }

        }

        private string opis;

        public event PropertyChangedEventHandler PropertyChanged;


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

        public NoviTip(string k)
        {
            baza = new BazaPodataka(k);
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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

        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (oznaka_textBox.Text != "" && naziv_textBox.Text != "" && slika != null)
            {
                Tip tip = new Tip(oznaka, naziv, opis, slika);
                bool passed = baza.novTip(tip);
                if (passed)
                {
                    System.Windows.MessageBox.Show("Uspešno dodavanje novog tipa.", "Uspeh!");
                    baza.sacuvajTip();
                    this.Close();
                }
                else
                    System.Windows.MessageBox.Show("Vec postoji tip sa tom oznakom!", "Greska!");
            }
            else
            {
                System.Windows.MessageBox.Show("Niste uneli sve obavezne podatke!", "Greska!");

            }
        }

        private void odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


             public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void naziv_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

