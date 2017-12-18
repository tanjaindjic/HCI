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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekat.Model;
using Projekat.Help;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using Projekat.Dijalozi;
using System.ComponentModel;
using Projekat.Tabele;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
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
            set { naziv = value; }
        }

        private Tip tip;
        public Tip Tip
        {
            get { return tip; }
            set { tip = value; }
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
            set { oznaka = value; }
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

        private Manifestacija m;
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
        public string Korinik { private set { korisnik = value; } get { return korisnik; } }


        private void ucitajSve()
        {
            foreach (Manifestacija m in baza.Manifestacije)
            {
                bool result = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result)
                    continue;
                if (m.X != -1 || m.Y != -1)
                {
                    Image img = new Image();
                    if(!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox pusenje = new TextBox();
                    pusenje.IsEnabled = false;
                    if (m.Pusenje)
                        pusenje.Text = "Pusenje: Dozvoljeno";
                    else
                        pusenje.Text = "Pusenje: Zabranjeno";
                    wp.Children.Add(pusenje);

                    TextBox invalidi = new TextBox();
                    invalidi.IsEnabled = false;
                    if (m.Invalidi)
                        invalidi.Text = "Hendikepirani: Ima pristup";
                    else
                        invalidi.Text = "Hendikepirani: Nema pristup";
                    wp.Children.Add(invalidi);


                    TextBox mesto = new TextBox();
                    mesto.IsEnabled = false;
                    mesto.Text = "Mesto: " + m.Mesto;
                    wp.Children.Add(mesto);

                    TextBox alkohol = new TextBox();
                    alkohol.IsEnabled = false;
                    alkohol.Text = "Alkohol: " + m.Alkohol;
                    wp.Children.Add(alkohol);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cene: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox publika = new TextBox();
                    publika.IsEnabled = false;
                    publika.Text = "Publika: " + m.Publika;
                    wp.Children.Add(publika);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;

                    canvasMap.Children.Add(img);
                    Canvas.SetLeft(img, m.X-20);
                    Canvas.SetTop(img, m.Y-20);
                }
            }
        }

        public MainWindow(string k)
        {
            korisnik = k;
            slika = null;
            datum = DateTime.Today;

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
            m = new Manifestacija();
            baza = new BazaPodataka(korisnik);
            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;  //glavni prozor se prikazuje u centru
            this.DataContext = this;
            System.Windows.Forms.MessageBoxManager.OK = "U redu";
            System.Windows.Forms.MessageBoxManager.Yes = "Da";
            System.Windows.Forms.MessageBoxManager.No = "Ne";
            System.Windows.Forms.MessageBoxManager.Cancel = "Odustani";

            System.Windows.Forms.MessageBoxManager.Register();

            baza.ucitajManifestacije();
            ucitajSve();

            manifList = baza.Manifestacije;
            baza.ucitajEtikete();
            baza.ucitajTipove();
            slika = "";
        }
        private ObservableCollection<string> alkohol;
        public ObservableCollection<string> Alkohol
        {
            get { return alkohol; }
            set { alkohol = value; }
        }
        private ObservableCollection<string> mesto;
        public ObservableCollection<string> Mesto
        {
            get { return mesto; }
            set { mesto = value; }
        }
        private ObservableCollection<string> publika;
        public ObservableCollection<string> Publika
        {
            get { return publika; }
            set { publika = value; }
        }
        private ObservableCollection<string> cena;
        private Point startPoint;

        public ObservableCollection<string> Cena
        {
            get { return cena; }
            set { cena = value; }
        }

        #region Click

        private void NovaManifestacija_Click(object sender, RoutedEventArgs e)
        {
            var s = new NovaManifestacija(korisnik);
            s.ShowDialog();
            /*Manifestacija m = s.m;
                        int count = baza.Manifestacije.Count - 1;
                        baza.Manifestacije[count] = m;
              */
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;
            manifestacijaBox.ItemsSource = manifList;
        }

        private void NoviTip_Click(object sender, RoutedEventArgs e)
        {
            var s = new NoviTip(korisnik);
            s.Show();
        }

        private void NovaEtiketa_Click(object sender, RoutedEventArgs e)
        {
            var s = new NovaEtiketa(korisnik);
            s.Show();
        }

        private void PregledManifestacija_Click(object sender, RoutedEventArgs e)
        {
            var s = new PregledManifestacija(korisnik);
            s.ShowDialog();
            if (s.idx != -1)
            {
                baza.Manifestacije[s.idx] = s.izmenjena;
                baza.sacuvajManifestaciju();
                manifList = baza.Manifestacije;
                Manifestacija m = s.izmenjena;
                Image zaMenjanje = null;
                int idx = 0;
                foreach (Image img in canvasMap.Children)
                {
                    if (img.Tag.Equals(m.Oznaka))
                    {
                        zaMenjanje = img;
                        break;
                    }
                    idx++;
                }
                if (zaMenjanje != null) {
                    
                        WrapPanel wp = new WrapPanel();
                        wp.Orientation = Orientation.Vertical;

                        TextBox oznaka = new TextBox();
                        oznaka.IsEnabled = false;
                        oznaka.Text = "Oznaka: " + m.Oznaka;
                        wp.Children.Add(oznaka);

                        TextBox naziv = new TextBox();
                        naziv.IsEnabled = false;
                        naziv.Text = "Naziv: " + m.Naziv;
                        wp.Children.Add(naziv);


                        TextBox tip = new TextBox();
                        tip.IsEnabled = false;
                        tip.Text = "Tip: " + m.Tip.Oznaka;
                        wp.Children.Add(tip);


                        TextBox opis = new TextBox();
                        opis.IsEnabled = false;
                        opis.Text = "Opis: " + m.Opis;
                        wp.Children.Add(opis);


                        TextBox datum = new TextBox();
                        datum.IsEnabled = false;
                        datum.Text = "Datum: " + m.Datum.ToShortDateString();
                        wp.Children.Add(datum);

                        TextBox pusenje = new TextBox();
                        pusenje.IsEnabled = false;
                        if (m.Pusenje)
                            pusenje.Text = "Pusenje: Dozvoljeno";
                        else
                            pusenje.Text = "Pusenje: Zabranjeno";
                        wp.Children.Add(pusenje);

                        TextBox invalidi = new TextBox();
                        invalidi.IsEnabled = false;
                        if (m.Invalidi)
                            invalidi.Text = "Hendikepirani: Ima pristup";
                        else
                            invalidi.Text = "Hendikepirani: Nema pristup";
                        wp.Children.Add(invalidi);


                        TextBox mesto = new TextBox();
                        mesto.IsEnabled = false;
                        mesto.Text = "Mesto: " + m.Mesto;
                        wp.Children.Add(mesto);

                        TextBox alkohol = new TextBox();
                        alkohol.IsEnabled = false;
                        alkohol.Text = "Alkohol: " + m.Alkohol;
                        wp.Children.Add(alkohol);

                        TextBox etikete = new TextBox();
                        etikete.IsEnabled = false;
                        etikete.Text = "Etikete:" + System.Environment.NewLine;
                        ListaEtiketa = "";
                        StringBuilder sb = new StringBuilder(ListaEtiketa);
                        ObservableCollection<Etiketa> list = m.Etikete;
                        foreach (Etiketa et in list)
                        {
                            sb.Append(et.Oznaka);
                            sb.Append(System.Environment.NewLine);
                        }
                        ListaEtiketa = sb.ToString();
                        etikete.Text += ListaEtiketa;
                        ListaEtiketa = "";
                        wp.Children.Add(etikete);

                        TextBox cena = new TextBox();
                        cena.IsEnabled = false;
                        cena.Text = "Cene: " + m.Cena;
                        wp.Children.Add(cena);

                        TextBox publika = new TextBox();
                        publika.IsEnabled = false;
                        publika.Text = "Publika: " + m.Publika;
                        wp.Children.Add(publika);

                        ToolTip tt = new ToolTip();
                        tt.Content = wp;
                         zaMenjanje.ToolTip = tt;
                    if(!m.Slika.Equals(""))
                        zaMenjanje.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        zaMenjanje.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    canvasMap.Children[idx] = zaMenjanje;
                        
                    }
            }
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;

            manifestacijaBox.ItemsSource = manifList;
            bool nePostoji = true;
            List<Image> zaBrisanje = new List<Image>();
            if (manifList.Count == 0) {
                int count = canvasMap.Children.Count;
                canvasMap.Children.RemoveRange(0,count);
                return;
            }
            foreach (Image img in canvasMap.Children)
            {
                foreach(Manifestacija ma in manifList)
                {
                    if (ma.Oznaka.Equals(img.Tag))
                        nePostoji = false;                   
                }
                if (nePostoji)
                {
                    zaBrisanje.Add(img);

                }
                nePostoji = true;
            }
            if (zaBrisanje != null) { 
            foreach(Image i in zaBrisanje)
                {
                    canvasMap.Children.Remove(i);
                }
            }
        }

        private void PregledTipova_Click(object sender, RoutedEventArgs e)
        {
            var s = new pregledTipova(korisnik);
            s.ShowDialog();
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;
            manifestacijaBox.ItemsSource = manifList;
            foreach(Manifestacija m in baza.Manifestacije)
            {
                foreach(Image img in canvasMap.Children)
                {
                    if (img.Tag.Equals(m.Oznaka) && m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));
                }
            }
            bool nePostoji = true;
            List<Image> zaBrisanje = new List<Image>();
            if (manifList.Count == 0)
            {
                int count = canvasMap.Children.Count;
                canvasMap.Children.RemoveRange(0, count);
            }
            foreach (Image img in canvasMap.Children)
            {
                foreach (Manifestacija ma in manifList)
                {
                    if (ma.Oznaka.Equals(img.Tag))
                        nePostoji = false;

                    if (nePostoji)
                    {
                        zaBrisanje.Add(img);
                        nePostoji = true;
                    }
                }
            }
            if (zaBrisanje != null)
            {
                foreach (Image i in zaBrisanje)
                {
                    canvasMap.Children.Remove(i);
                }
            }
        }

        private void PregledEtiketa_Click(object sender, RoutedEventArgs e)
        {
            var s = new pregledEtiketa(korisnik);
            s.ShowDialog();
           
        }


       
        private void About_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.Help.ShowHelp(null, @"Pomoc.chm");
        }

        #endregion Click

        private void Window_Help_Executed(object sender, ExecutedRoutedEventArgs e)

        {

            FrameworkElement source = e.Source as FrameworkElement;

            if (source != null)

            {

                string helpString = HelpProvider.GetHelpString(source);

                if (helpString != null)

                {

                    System.Windows.Forms.Help.ShowHelp(null, @"Pomoc.chm", System.Windows.Forms.HelpNavigator.KeywordIndex, helpString);


                }

            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void odabirIkonice_Click(object sender, RoutedEventArgs e)
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

        private void btnIkonica_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            var s = new Login();
            this.Close();
            s.ShowDialog();

        }

        private void sacuvaj_Click(object sender, RoutedEventArgs e)
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
            ObservableCollection<Etiketa>  listaEtiketa = etikete;

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
            
            datum = (DateTime)datumPicker.SelectedDate;
            if(oznaka_textBox.Text.Equals("") || naziv_textBox.Text.Equals("") || tip_textBox.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Niste uneli neophodne podatke!", "Dodavanje manifestacije");
                return;
            }
            Manifestacija m = new Manifestacija(oznaka, naziv, opis, tip, alkohol, invalidi, pusenje, cena, datum, etikete, slika, mesto, publika);
            bool passed = baza.novaManifestacija(m);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno ste dodali novu manifestaciju.", "Dodavanje manifestacije");
                baza.sacuvajManifestaciju();
                manifList = baza.Manifestacije;
                manifestacijaBox.ItemsSource = manifList;
                
                ikonica.Source = new BitmapImage(new Uri(@"Images\placeholder.png", UriKind.Relative));
                slika = "";

            }
            else
                System.Windows.MessageBox.Show("Vec postoji manifestacija sa tom oznakom!", "Dodavanje manifestacije");
        }


        private void dropOnMe_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent("manifestacija"))
            {
                Manifestacija m = e.Data.GetData("manifestacija") as Manifestacija;

                bool result = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
              //  bool preklapanje = false;
                System.Windows.Point d0 = e.GetPosition(canvasMap);
                foreach (Manifestacija r0 in baza.Manifestacije)
                {
                    if (m.Oznaka != r0.Oznaka) //ako hoce da pomeri manifesatciju jako malo da ne okida dole
                    {
                        if (r0.X != -1 && r0.Y != -1)
                        {
                            if (Math.Abs(r0.X - d0.X) <= 30 && Math.Abs(r0.Y - d0.Y) <= 30)
                            {
                                System.Windows.MessageBox.Show("Manifestacija sa ovom lokacijom već postoji na mapi! Ponovo unesite manifestaciju na mapu.", "Premeštanje manifestacije na mapi");
                                //  preklapanje = true;
                                /*  int idx = 0;
                                  foreach (Manifestacija ma in baza.Manifestacije)
                                  {
                                      if (ma.Oznaka.Equals(m.Oznaka))
                                          break;
                                      idx++;
                                  }
                                  baza.Manifestacije[idx].X = -1;
                                  baza.Manifestacije[idx].Y = -1;
                                  baza.sacuvajManifestaciju();
                                  */
                                ucitajSve();
                                return;
                            }
                        }
                    }
                }
                if (result == false)
                {

                    Image img = new Image();
                    if(!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    var positionX = e.GetPosition(canvasMap).X;
                    var positionY = e.GetPosition(canvasMap).Y;
                    //if (preklapanje == false) { 
                        m.X = positionX;
                        m.Y = positionY;
                    //}
                    
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox pusenje = new TextBox();
                    pusenje.IsEnabled = false;
                    if (m.Pusenje)
                        pusenje.Text = "Pusenje: Dozvoljeno";
                    else
                        pusenje.Text = "Pusenje: Zabranjeno";
                    wp.Children.Add(pusenje);

                    TextBox invalidi = new TextBox();
                    invalidi.IsEnabled = false;
                    if (m.Invalidi)
                        invalidi.Text = "Hendikepirani: Ima pristup";
                    else
                        invalidi.Text = "Hendikepirani: Nema pristup";
                    wp.Children.Add(invalidi);


                    TextBox mesto = new TextBox();
                    mesto.IsEnabled = false;
                    mesto.Text = "Mesto: " + m.Mesto;
                    wp.Children.Add(mesto);

                    TextBox alkohol = new TextBox();
                    alkohol.IsEnabled = false;
                    alkohol.Text = "Alkohol: " + m.Alkohol;
                    wp.Children.Add(alkohol);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cene: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox publika = new TextBox();
                    publika.IsEnabled = false;
                    publika.Text = "Publika: " + m.Publika;
                    wp.Children.Add(publika);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;
                    ObservableCollection<Manifestacija> manifestLst = baza.Manifestacije;
                    int idx = 0;
                    foreach (Manifestacija ma in manifestLst)
                    {
                        if (ma.Oznaka.Equals(m.Oznaka))
                            break;
                        idx++;
                    }
                    manifestLst[idx] = m;
                    baza.Manifestacije = manifestLst;//ovo dodato
                    baza.sacuvajManifestaciju();
                    canvasMap.Children.Add(img);
                    Canvas.SetLeft(img, positionX-20);
                    Canvas.SetTop(img, positionY-20);
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("Manifestacija sa ovom oznakom već postoji na mapi!", "Dodavanje manifestacije na mapu");

                }
            }
 //  canvasMap.Children.Add();
        }
         private void ukloniSaMape()
        {

        }

      
        private void DraggedImageMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {

                
                Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedValue;

                if (selektovana != null)
                {
                    Image img = sender as Image;
                    canvasMap.Children.Remove(img);
                    DataObject dragData = new DataObject("manifestacija", selektovana);
                    DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                    
                }

            }
          
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {


            if (!e.Data.GetDataPresent("manifestacija") || sender == e.Source)
            {

                e.Effects = DragDropEffects.None;
            }
           
        }


        private void odabirEtiketa_Click(object sender, RoutedEventArgs e)
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

                if(temp!= null && temp.Count>0)
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


        private void odabirTipa_Click(object sender, RoutedEventArgs e)
        {
            odabirTipa s = new odabirTipa(korisnik);
            s.ShowDialog();

            //ovde odmah nastavlja dalje umesto da saceka na odabir tipa i zato je tip dole uvek null
            Tip temp = s.Odabran;
            if (temp != null)
            {
                tip = temp;
                oznakaTipa = tip.Oznaka;
                tip_textBox.Text = OznakaTipa;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (manifestacijaBox.SelectedValue is Manifestacija)
            {
                Manifestacija m = (Manifestacija)manifestacijaBox.SelectedValue;
                if (!m.Slika.Equals(""))
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                else
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));
                 
            }
            else { PrikazIkonice.Source = null; }
           
            

        }//MS.Internal.NamedObject

        private void PrikazIkonice_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                try { 
                    Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedValue;
                    if (selektovana != null)
                    {
                        DataObject dragData = new DataObject("manifestacija", selektovana);
                        DragDrop.DoDragDrop(PrikazIkonice, dragData, DragDropEffects.Move);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void PrikazIkonice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DraggedImagePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Manifestacija man in ManifList)
            {
                if (man.Oznaka.Equals(img.Tag)) { 
                    manifestacijaBox.SelectedValue = man;
                    if (!man.Slika.Equals(""))
                        PrikazIkonice.Source = new BitmapImage(new Uri(man.Slika));
                    else
                        PrikazIkonice.Source = new BitmapImage(new Uri(man.Tip.Slika));

                }
            }
            if (e.LeftButton == MouseButtonState.Released)
                e.Handled=true;

        }

        private void DraggedImagePreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Manifestacija man in ManifList)
            {
                if (man.Oznaka.Equals(img.Tag))
                    manifestacijaBox.SelectedValue = man;
            }
           

        }

        private void izmeniManifestacijuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manifestacijaBox.SelectedValue is Manifestacija)
            {
                Manifestacija m = (Manifestacija)manifestacijaBox.SelectedValue;


                var s = new izmeniManifestaciju(m, korisnik);
                s.ShowDialog();
                if (s.idx != -1)
                {
                    baza.Manifestacije[s.idx] = s.Izmenjena;
                    baza.sacuvajManifestaciju();
                    manifList = baza.Manifestacije;
                    Manifestacija mm = s.Izmenjena;
                    Image zaMenjanje = null;
                    int idx = 0;
                    foreach (Image img in canvasMap.Children)
                    {
                        if (img.Tag.Equals(m.Oznaka))
                        {
                            zaMenjanje = img;
                            break;
                        }
                        idx++;
                    }
                    if (zaMenjanje != null)
                    {

                        WrapPanel wp = new WrapPanel();
                        wp.Orientation = Orientation.Vertical;

                        TextBox oznaka = new TextBox();
                        oznaka.IsEnabled = false;
                        oznaka.Text = "Oznaka: " + mm.Oznaka;
                        wp.Children.Add(oznaka);

                        TextBox naziv = new TextBox();
                        naziv.IsEnabled = false;
                        naziv.Text = "Naziv: " + mm.Naziv;
                        wp.Children.Add(naziv);


                        TextBox tip = new TextBox();
                        tip.IsEnabled = false;
                        tip.Text = "Tip: " + mm.Tip.Oznaka;
                        wp.Children.Add(tip);


                        TextBox opis = new TextBox();
                        opis.IsEnabled = false;
                        opis.Text = "Opis: " + mm.Opis;
                        wp.Children.Add(opis);


                        TextBox datum = new TextBox();
                        datum.IsEnabled = false;
                        datum.Text = "Datum: " + mm.Datum;
                        wp.Children.Add(datum);

                        TextBox pusenje = new TextBox();
                        pusenje.IsEnabled = false;
                        if (mm.Pusenje)
                            pusenje.Text = "Pusenje: Dozvoljeno";
                        else
                            pusenje.Text = "Pusenje: Zabranjeno";
                        wp.Children.Add(pusenje);

                        TextBox invalidi = new TextBox();
                        invalidi.IsEnabled = false;
                        if (mm.Invalidi)
                            invalidi.Text = "Hendikepirani: Ima pristup";
                        else
                            invalidi.Text = "Hendikepirani: Nema pristup";
                        wp.Children.Add(invalidi);


                        TextBox mesto = new TextBox();
                        mesto.IsEnabled = false;
                        mesto.Text = "Mesto: " + mm.Mesto;
                        wp.Children.Add(mesto);

                        TextBox alkohol = new TextBox();
                        alkohol.IsEnabled = false;
                        alkohol.Text = "Alkohol: " + mm.Alkohol;
                        wp.Children.Add(alkohol);

                        TextBox etikete = new TextBox();
                        etikete.IsEnabled = false;
                        etikete.Text = "Etikete:" + System.Environment.NewLine;
                        ListaEtiketa = "";
                        StringBuilder sb = new StringBuilder(ListaEtiketa);
                        ObservableCollection<Etiketa> list = mm.Etikete;
                        foreach (Etiketa et in list)
                        {
                            sb.Append(et.Oznaka);
                            sb.Append(System.Environment.NewLine);
                        }
                        ListaEtiketa = sb.ToString();
                        etikete.Text += ListaEtiketa;
                        ListaEtiketa = "";
                        wp.Children.Add(etikete);

                        TextBox cena = new TextBox();
                        cena.IsEnabled = false;
                        cena.Text = "Cene: " + mm.Cena;
                        wp.Children.Add(cena);

                        TextBox publika = new TextBox();
                        publika.IsEnabled = false;
                        publika.Text = "Publika: " + mm.Publika;
                        wp.Children.Add(publika);

                        ToolTip tt = new ToolTip();
                        tt.Content = wp;
                        zaMenjanje.ToolTip = tt;
                        if(!mm.Slika.Equals(""))
                            zaMenjanje.Source = new BitmapImage(new Uri(mm.Slika));
                        else
                            zaMenjanje.Source = new BitmapImage(new Uri(mm.Tip.Slika));

                        canvasMap.Children[idx] = zaMenjanje;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijednu manifestaciju.", "Izmena manifestacije");

            }
        }

        private void obrisiManifestacijuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manifestacijaBox.SelectedItems.Count == 1) {
                Manifestacija m = null;
                if (manifestacijaBox.SelectedValue is Manifestacija)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete manifestaciju?", "Brisanje manifestacije", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            
                            m = (Manifestacija)manifestacijaBox.SelectedValue;
                            baza.brisanjeManifestacije(m);
                            Image zaBrisanje = null;
                            foreach (Image img in canvasMap.Children)
                            {
                                if (m.Oznaka.Equals(img.Tag))
                                {
                                    zaBrisanje = img;
                                }
                            }
                            if (zaBrisanje != null)
                                canvasMap.Children.Remove(zaBrisanje);
                            manifList = baza.Manifestacije;
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }


                }
            }
            else if (manifestacijaBox.SelectedItems.Count > 1)
            {
                ObservableCollection<Manifestacija> list = new ObservableCollection<Manifestacija>();
                Manifestacija m = null;
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete manifestacije?", "Brisanje manifestacija", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try { 
                            foreach (Manifestacija ma in manifestacijaBox.SelectedItems)
                            {
                                if(ma is Manifestacija)
                                    list.Add(ma);
                            }
                            foreach (Manifestacija ma in list) { 
                                m = ma;
                                baza.brisanjeManifestacije(m);
                                Image zaBrisanje = null;
                                foreach (Image img in canvasMap.Children)
                                {
                                    if(m.Oznaka.Equals(img.Tag))
                                    {
                                        zaBrisanje = img;
                                    }
                                }
                                if (zaBrisanje != null)
                                    canvasMap.Children.Remove(zaBrisanje);
                            }
                            manifList = baza.Manifestacije;
                            break;
                        }
                        catch
                        {
                            //System.Windows.MessageBox.Show("Odabrana je prazna manifestacija za brisanje!", "Brisanje manifestacije");
                            return;
                        }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedItem;
                if (selektovana != null)
                {
                    bool postoji = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == selektovana.Oznaka);
                    if (postoji)
                    {
                        Image img = null;
                        foreach(Image i in canvasMap.Children)
                        {
                            if (i.Tag.Equals(selektovana.Oznaka))
                                img = i;
                        }
                        canvasMap.Children.Remove(img);
                        int idx = 0;
                        foreach (Manifestacija m in manifList)
                        {
                            if (selektovana.Oznaka.Equals(m.Oznaka))
                                break;
                            idx++;
                        }
                        baza.Manifestacije[idx].X = -1;
                        baza.Manifestacije[idx].Y = -1;
                        baza.sacuvajManifestaciju();
                        baza.ucitajManifestacije();
                        manifList = baza.Manifestacije;
                        ucitajSve();
                    }
                }
            }
            catch
            {
                return;
            }
            
        }

        private void obrisiSve_Click(object sender, RoutedEventArgs e)
        {
            naziv_textBox.Text = "";
            tip_textBox.Text = "";
            oznaka_textBox.Text = "";
            datumPicker.SelectedDate = DateTime.Now;
            etikete_textBox.Text = "";
            opis_textBox.Text = "";
            mesto_comboBox.SelectedIndex = -1;
            publika_comboBox.SelectedIndex = -1;
            cene_comboBox.SelectedIndex = -1;
            alkohol_comboBox.SelectedIndex = -1;
            ikonica.Source = new BitmapImage(new Uri(@"Images\placeholder.png", UriKind.Relative));
            pusenjeDa.IsChecked = false;
            pusenjeNe.IsChecked = false;
            invalidiDa.IsChecked = false;
            invalidiNe.IsChecked = false;
        }
    }
}

