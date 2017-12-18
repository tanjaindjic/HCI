using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for odabirEtikete.xaml
    /// </summary>
    public partial class odabirEtikete : Window
    {
        private BazaPodataka baza;
        private ObservableCollection<Etiketa> etikete;

        public ObservableCollection<Etiketa> Etikete
        {
            get { return etikete; }
            set { etikete = value; }
        }

        private ObservableCollection<Etiketa> odabrana;

        public ObservableCollection<Etiketa> Odabrana
        {
            get { return odabrana; }
            set { odabrana = value; }
        }
        public bool dodavanje;

        public odabirEtikete(string k)
        {
            baza = new BazaPodataka(k);
            baza.ucitajEtikete();
            etikete = baza.Etikete;
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        private void izaberi_click(object sender, RoutedEventArgs e)
        {
            dodavanje = true;
            odabrana = new ObservableCollection<Etiketa>();
            if (dgrMain.SelectedItems != null) { 
                foreach(Etiketa et in dgrMain.SelectedItems) { 
                    odabrana.Add(et);
                }
            }
            else
                odabrana = null;
            this.Close();
        }

        private void odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ukloni_Click(object sender, RoutedEventArgs e)
        {
            dodavanje = false;
            odabrana = new ObservableCollection<Etiketa>();
            if (dgrMain.SelectedItems != null)
            {
                foreach (Etiketa et in dgrMain.SelectedItems)
                {
                    odabrana.Add(et);
                }
            }
            else
                odabrana = null;
            this.Close();
        }
    }
    
}
