using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;


namespace májusi_progfeladat
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            jarmuvek = new List<Jarmu>();
        }
        string fajlnev = @"adat.txt";

        private void fooldal_Initialized(object sender, EventArgs e)
        {

            string[] adatsor = File.ReadAllLines(fajlnev);
            foreach (string adat in adatsor)
            {
                string[] rész = adat.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (rész.Length >= 2)
                {
                    nametag.Text = rész[0];
                }

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nametag.Clear();
            MainWindow vissza = new MainWindow();
            this.Visibility = Visibility.Hidden;
            vissza.Show();
        }

        private List<Jarmu> jarmuvek;
        string allomas = "allomas";
        private void keszthely_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Keszthely";
            
         }
        private void badacsony_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Badacsony";
        }

        private void zánka_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Zánka";
        }

        private void balatonfüred_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Tihany";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonfüred";
        }

        private void balatonalmádi_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonalmádi";
        }

        private void balatonfűzfő_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonfűzfő";
        }

        private void balatonakarattya_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonakarattya";
        }

        private void siófok_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Siófok";
        }

        private void balatonföldvár_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonföldvár";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonszemes";
        }

        private void fonyód_Click(object sender, RoutedEventArgs e)
        {
            allomas = "Fonyód";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            allomas = "Balatonszentgyörgy";
        }



        private void ButtonHozzaad_Click(object sender, RoutedEventArgs e)
        {
            
            if (ComboBoxFajta.SelectedIndex != -1 && !string.IsNullOrEmpty(TextBoxFogyasztas.Text))
            {
                string fajta = (ComboBoxFajta.SelectedItem as ComboBoxItem)?.Content.ToString();
                double fogyasztas = double.Parse(TextBoxFogyasztas.Text);
                double töl = double.Parse(tol.Text);
                double íg = double.Parse(ig.Text);
                double egyszkm = double.Parse(hatotav.Text) / 100;
                double maxtav = íg - töl;
                

                
                Jarmu jarmu = new Jarmu(fajta, fogyasztas, töl, íg, egyszkm, maxtav, allomas);
                jarmuvek.Add(jarmu);

                ComboBoxFajta.SelectedIndex = -1;
                TextBoxFogyasztas.Clear();
                hatotav.Clear();
                tol.Clear();
                ig.Clear();
                


            }

            else if (ComboBoxFajta.SelectedIndex == -1)
            {
                MessageBox.Show("Kérlek válaszd ki a jármű fajtáját!");
            }

            else if (TextBoxFogyasztas.Text == "")
            {
                MessageBox.Show("Kérlek add meg a jármű fogyasztását!");
            }
        }

        private void ButtonKiir_Click(object sender, RoutedEventArgs e)
        {
            var csoportositasfajtaszerint = jarmuvek.GroupBy(j =>
            {
                if (j.Fajta == "Autó")
                    return "Autó(k)";

                if (j.Fajta == "Kerékpár")
                    return "Kerékpár(ok)";

                if (j.Fajta == "Roller")
                    return "Roller(ek)";

                return string.Empty;
            });

            string eredmeny = "Köszönjük hogy a mi állomásunkat választotta:\n\n";


            var autok = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Autó(k)");
            if (autok != null)
            {

                
                foreach (var j in autok)
                {
                    double egyszKwh = j.Egyszkm * j.Fogyasztas;
                    double maxtavKwh = egyszKwh * j.Maxtav;
                    double ar = maxtavKwh * 70;


                    string kekeritettar = string.Format("{0:0.00}", ar);
                    eredmeny += "A(z) " + j.Fajta + " " + kekeritettar + "Ft-ért " + maxtavKwh + " kWh-ot töltött a " + allomas + "i állomáson.";
                }
                eredmeny += "\n";
            }

            var kerekparok = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Kerékpár(ok)");
            if (kerekparok != null)
            {
                
                foreach (var j in kerekparok)
                {
                    double egyszKwh = j.Egyszkm * j.Fogyasztas;
                    double maxtavKwh = egyszKwh * j.Maxtav;
                    double ar = maxtavKwh * 70;

                    string kekeritettar = string.Format("{0:0.00}",ar);
                    eredmeny += "A(z) "+j.Fajta + " " + kekeritettar + "Ft-ért "+ maxtavKwh + " kWh-ot töltött a " +allomas+"i állomáson.";
                }
                eredmeny += "\n";
            }

            var rollerek = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Roller(ek)");
            if (rollerek != null)
            {
                
                foreach (var j in rollerek)
                {
                    double egyszKwh = j.Egyszkm * j.Fogyasztas;
                    double maxtavKwh = egyszKwh * j.Maxtav;
                    double ar = maxtavKwh * 70;

                    string kekeritettar = string.Format("{0:0.00}",ar);
                    eredmeny += "A(z) " + j.Fajta + " " + kekeritettar + "Ft-ért " + maxtavKwh + " kWh-ot töltött a " + allomas + "i állomáson.";
                }
                eredmeny += "\n";
            }
            Window2 ujablak = new Window2();
            this.Visibility = Visibility.Hidden;
            ujablak.Show();


            MessageBox.Show(eredmeny);
        }



        public class Jarmu
        {
            public string Fajta { get; set; }
            public double Fogyasztas { get; set; }

            public double Től { get; set; }

            public double Ig { get; set; }

            public double Egyszkm { get; set; }

            public double Maxtav { get; set; }
            public string Allomas { get; set; }


            public Jarmu(string fajta, double fogyasztas, double töl, double íg, double egyszkm, double maxtav, string allomas)
            {
                Fajta = fajta;
                Fogyasztas = fogyasztas;
                Től = töl;
                Ig = íg;
                Egyszkm = egyszkm;
                Maxtav = maxtav;
                Allomas = allomas;

            }
        }

        
    }
}


















