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
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;

namespace májusi_progfeladat
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                toltobar.Value++;
                Thread.Sleep(100);
            }

        }
    }
}
