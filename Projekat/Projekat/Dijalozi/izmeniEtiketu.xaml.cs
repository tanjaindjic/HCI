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
    /// Interaction logic for izmeniEtiketu.xaml
    /// </summary>
    public partial class izmeniEtiketu : INotifyPropertyChanged
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
       

        private System.Drawing.Color boja;

        public System.Drawing.Color Boja
        {
            get { return boja; }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
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

        private System.Windows.Media.Color mediacolor;
        public System.Windows.Media.Color Mediacolor
        {
            get { return mediacolor; }
            set
            {
                if (value != mediacolor)
                {
                    mediacolor = value;
                    OnPropertyChanged("Mediacolor");
                }
            }
        }


        private BazaPodataka baza;
        private Etiketa selektovana;
        public izmeniEtiketu(Etiketa m, string k)
        {
            baza = new BazaPodataka(k);
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            selektovana = m;
            Oznaka = m.Oznaka;
            Opis = m.Opis;
            boja = m.Boja;
            Mediacolor = System.Windows.Media.Color.FromArgb(boja.A, boja.R, boja.G, boja.B);
        }


        private void colorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Mediacolor = ColorPicker.SelectedColor.Value;
            Boja = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);


        }

        private void odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int idx = -1;
        public Etiketa izmenjena;
        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {
                izmenjena = new Etiketa(oznaka, opis, boja);
           
                baza.ucitajEtikete();
                idx = 0;
                foreach (Etiketa man in baza.Etikete)
                {
                    if (man.Oznaka == izmenjena.Oznaka)
                        break;
                    idx++;
                }
                baza.Etikete[idx] = izmenjena;
                baza.sacuvajEtiketu();
                

                System.Windows.MessageBox.Show("Uspešna izmena etikete!", "Izmena etikete");
                this.Close();
            
        }

    }

}

