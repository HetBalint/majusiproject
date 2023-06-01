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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Security.Cryptography;

namespace májusi_progfeladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string key = "fk%eks1v9cm@";
        string fajlnev = @"adat.txt";
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int szamol = 0;
            for (int i=0;i<jelszo.Text.Length; i++)
            {
                szamol++;
            }
            if (szamol < 5)
            {
                message.Content = "A jelszónak minimum 5 karaktert kell tartalmaznia.";
            }
            else
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(jelszo.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        jelszo.Text = Convert.ToBase64String(results, 0, results.Length);


                    }
                }
                

                if (File.Exists(fajlnev))
                {
                    File.AppendAllText(fajlnev, "\n" + felhasznalonev.Text + "," + jelszo.Text);
                }


                /*byte[] decriptor = Convert.FromBase64String(jelszo.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(decriptor, 0, decriptor.Length);
                        txtEncrypt.Text = UTF8Encoding.UTF8.GetString(results);


                    }
                }
                */


                message.Content = "Sikeres regisztráció!";
                felhasznalonev.Clear();
                jelszo.Clear();
            }

            








        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            var users = new System.Collections.Generic.Dictionary<string, string>();




            string[] adatsor = File.ReadAllLines(fajlnev);
            foreach (string adat in adatsor)
            {
                string[] rész = adat.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (rész.Length >= 2)
                {
                    //rész[1] re kell átírni ha rossz//
                    users.Add(rész[0], rész[1]);
                }

                string username = felhasznalonev.Text;
                string password = jelszo.Text;
                

                byte[] data = UTF8Encoding.UTF8.GetBytes(jelszo.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        password = Convert.ToBase64String(results, 0, results.Length);


                    }
                }


                if (users.ContainsKey(username) && users[username]==password)
                 {
                    

                    message.Content = "Sikeres belépés!";
                    felhasznalonev.Clear();
                    jelszo.Clear();
                    Window1 ujablak = new Window1();
                    this.Visibility = Visibility.Hidden;
                    ujablak.Show();
                }
                else
                {
                    message.Content = "Hibás felhasználónév vagy jelszó!";
                }

            }
        }
    }
}