using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Projekat.Model
{
  public  class Manifestacija : INotifyPropertyChanged
    {
        private string oznaka = "";
        private string naziv = "";
        private string opis = "";
        private Tip tip = new Tip();
        private string alkohol = "";
        private bool invalidi = false;
        private bool pusenje = false;
        private string publika = "";
        private string cena="";
        private DateTime datum = DateTime.Today;
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();
        private string slika = "";
        private double x = -1;
        private double y = -1;
        private string mesto = "";


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

        public Tip Tip
        {
            get
            {
                return tip;
            }
            set
            {
                if (value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        public string Alkohol
        {
            get
            {
                return alkohol;
            }
            set
            {
                if (value != alkohol)
                {
                    alkohol = value;
                    OnPropertyChanged("Alkohol");
                }
            }
        }

        public bool Invalidi
        {
            get
            {
                return invalidi;
            }
            set
            {
                if (value != invalidi)
                {
                    invalidi = value;
                    OnPropertyChanged("Invalidi");
                }
            }
        }

        public bool Pusenje
        {
            get
            {
                return pusenje;
            }
            set
            {
                if (value != pusenje)
                {
                    pusenje = value;
                    OnPropertyChanged("Pusenje");
                }
            }
        }

        public string Publika
        {
            get
            {
                return publika;
            }
            set
            {
                if (value != publika)
                {
                    publika = value;
                    OnPropertyChanged("Publika");
                }
            }
        }

        public string Cena
        {
            get
            {
                return cena;
            }
            set
            {
                if (value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }


        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public ObservableCollection<Etiketa>  Etikete
        {
            get
            {
                return etikete;
            }
            set
            {
                if (value != etikete)
                {
                    etikete = value;
                    OnPropertyChanged("Etikete");
                }
            }
        }

        public String Slika
        {
            get
            {
                return slika;
            }
            set
            {
                if (value != slika)
                    slika = value;
                OnPropertyChanged("Slika");
            }
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value != x)
                    x = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value != y)
                    y = value;
                OnPropertyChanged("Y");
            }
        }


        public string Mesto
        {
            get
            {
                return mesto;
            }
            set
            {
                if (value != mesto)
                {
                    mesto = value;
                    OnPropertyChanged("Mesto");
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;


        public Manifestacija()
        {

            /*oznaka = "proba";
            naziv = "proba";
            opis = "proba";
            alkohol = Alkohol;
            invalidi = true;
            pusenje = true;
            publika = OcekivanaPublika.MLADI;
            Tip t = new Tip();
            t.Naziv = "proba";
            t.Opis = "proba";
            t.Oznaka = " proba";
            t.Slika = "proba";
            tip = t;
            cena = Cene.VISOKE;
            datum = DateTime.Today;
            System.Windows.Media.Color c = FromArgb(255, 155, 0, 255);
            Etiketa e = new Etiketa("proba", "proba", c);
            etikete.Add(e);
            x = 100;
            y = 100;
            slika = "proba";
            mesto = MestoOdrzavanja.NA_OTVORENOM;
            */


        }

        public Manifestacija(string o, string n, string op, Tip t, string al, bool h, bool p, string c, DateTime d, ObservableCollection<Etiketa>  l, string sl, string m, string pu)
        {

            this.oznaka = o;
            this.naziv = n;
            this.opis = op;
            this.tip = t;
            this.alkohol = al;
            this.invalidi = h;
            this.pusenje = p;
            this.cena = c;
            this.datum = d;
            this.etikete = l;
            this.slika = sl;
            this.mesto = m;
            this.publika = pu;


        }


        public void setAll(Manifestacija m)
        {


            oznaka = m.oznaka;
            naziv = m.naziv;
            opis = m.opis;
            tip = m.tip;
            alkohol = m.alkohol;
            invalidi = m.invalidi;
            pusenje = m.pusenje;
            publika = m.publika;
            cena = m.cena;
            datum = m.datum;
            etikete = m.etikete;
            x = m.x;
            y = m.y;
            slika = m.slika;
            mesto = m.mesto;

        }

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public bool addEtiketa(Etiketa e)
        {
            foreach (Etiketa e1 in etikete)
            {
                if (e1.Oznaka == e.Oznaka)
                {
                    etikete.Remove(e);
                    return true;
                }
            }

            return false;
        }


    }
}

