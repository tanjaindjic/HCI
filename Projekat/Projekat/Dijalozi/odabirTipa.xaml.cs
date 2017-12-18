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
using System.Collections.ObjectModel;

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for odabirTipa.xaml
    /// </summary>
    public partial class odabirTipa : Window
    {
        private BazaPodataka baza;
        private ObservableCollection<Model.Tip> tipovi;

        public ObservableCollection<Model.Tip> Tipovi
        {
            get { return tipovi; }
            set { tipovi = value; }
        }

        private Tip odabran;

        public Tip Odabran
        {
            get { return odabran; }
            set { odabran = value; }
        }
        public odabirTipa(string k)
        {
            baza = new BazaPodataka(k);
            baza.ucitajTipove();
            tipovi = baza.Tipovi;
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }


        private void odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
             
        }

        private void izaberi_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedItem != null)
                odabran = (Tip)dgrMain.SelectedItem;
            else
                odabran = null;
            this.Close();
        }

        private void ukloni_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedItems.Count == 1)
            {
                Tip m = null;
                if (dgrMain.SelectedValue is Manifestacija)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete tip?", "Brisanje tipa", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:

                            m = (Tip)dgrMain.SelectedValue;
                            baza.brisanjeTipa(m);
                            baza.ucitajTipove();
                            tipovi = baza.Tipovi;
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }


                }
            }
            else if (dgrMain.SelectedItems.Count > 1)
            {
                ObservableCollection<Tip> list = new ObservableCollection<Tip>();
                Tip m = null;
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete tipove?", "Brisanje tipova", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            foreach (Tip ma in dgrMain.SelectedItems)
                            {
                                if (ma is Tip)
                                    list.Add(ma);
                            }
                            foreach (Tip ma in list)
                            {
                                m = ma;
                                baza.brisanjeTipa(m);
                                
                            }
                            baza.ucitajTipove();
                            tipovi = baza.Tipovi;
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
                System.Windows.MessageBox.Show("Niste odabrali tip za brisanje!", "Brisanje tipa");

            }
        }
    }
}
