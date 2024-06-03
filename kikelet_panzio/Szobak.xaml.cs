using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace kikelet_panzio
{
    public partial class Szobak : Page
    {
        int index = -1;
        public Szobak()
        {
            InitializeComponent();
            foreach (Szoba szoba in MainWindow.szobak)
            {
                szobaId.Items.Add(szoba.Szobaszam);
            }
        }

        private void szobaId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = szobaId.SelectedIndex;
            ferohely.Text = MainWindow.szobak[index].Ferohely.ToString();
            ar.Text = MainWindow.szobak[index].Arperfo.ToString();
        }

        private void valtoztat_Click(object sender, RoutedEventArgs e)
        {
            int ujFero;
            int ujAr;
            if (ferohely.Text != "" && ar.Text != "" && int.TryParse(ferohely.Text, out ujFero)  && int.TryParse(ar.Text, out ujAr))
            {
                if (new int[] {2, 3, 4 }.Contains(ujFero) && ujAr > 5999 && ujAr <12001)
                {
                    MainWindow.szobak[index].Ferohely = Convert.ToInt32(ferohely.Text);
                    MainWindow.szobak[index].Arperfo = Convert.ToInt32(ar.Text);
                    MainWindow.KiirSzobak();
                    MessageBox.Show("Sikeres változtatás");
                }
                else
                {
                    MessageBox.Show("Nem megfelelő érték!");
                }
            }
            else
            {
                MessageBox.Show("Hibás adat!");
            }
        }
    }
}