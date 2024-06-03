using System;
using System.Collections.Generic;
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
    public partial class Foglalasok : Page
    {
        public Foglalasok()
        {
            InitializeComponent();
            elokeszit();
        }
        private void elokeszit()
        {
            for (int i = 0; i < MainWindow.foglalasok.Count; i++)
            {
                foglalasSorszam.Items.Add(i + 1);
            }
            for (int i = 0; i < MainWindow.szobak.Count; i++)
            {
                szobaId.Items.Add(MainWindow.szobak[i].Szobaszam);
            }
            for (int i = 0; i < MainWindow.ugyfelek.Count; i++)
            {
                ugyfelId.Items.Add(MainWindow.ugyfelek[i].Azon);


            }
            allapot.Items.Add("előjegyzett");
            allapot.Items.Add("teljesült");
            allapot.Items.Add("lemondott");
            muvelet.Items.Add("Módosítás");
            muvelet.Items.Add("Hozzáadás");
            muvelet.Items.Add("Törlés");
            muvelet.SelectedIndex = 1;
        }

        private void foglalasSorszam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = foglalasSorszam.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            szobaId.SelectedIndex = MainWindow.foglalasok[index].Szobaszam - 1;
            ugyfelId.SelectedItem = MainWindow.foglalasok[index].UgyfelAzon;

            erkezes.SelectedDate = MainWindow.foglalasok[index].Erkezes.ToDateTime(TimeOnly.MinValue);
            tavozas.SelectedDate = MainWindow.foglalasok[index].Tavozas.ToDateTime(TimeOnly.MinValue);

            fo.Text = MainWindow.foglalasok[index].Fo.ToString();

            bool kedvezmenyes = MainWindow.ugyfelek.Where(x => x.Azon == MainWindow.foglalasok[index].UgyfelAzon).First().Vip;

            allapot.SelectedItem = MainWindow.foglalasok[foglalasSorszam.SelectedIndex].Allapot;
        }

        private void fo_TextChanged(object sender, TextChangedEventArgs e)
        {
            szamitAr();
        }
        bool atugor = false;
        private void szamitAr()
        {
            if (atugor)
            {
                atugor = false;
                return;
            }
            int napok = (int)Convert.ToDateTime(tavozas.SelectedDate).Subtract(Convert.ToDateTime(erkezes.SelectedDate)).TotalDays;

            if (napok < 1)
            {
                return;
            }
            int fok = 0;
            if (!int.TryParse(fo.Text, out fok) || szobaId.SelectedIndex == -1 || ugyfelId.SelectedIndex == -1)
            {
                if (fo.Text == "")
                {
                    return;
                }
                //MessageBox.Show("Nincs minden szükséges adat megadva!");
                return;
            }
            int ferohely = MainWindow.szobak[Convert.ToInt32(szobaId.SelectedItem) - 1].Ferohely;
            if (fok < 1 || ferohely < fok)
            {
                //MessageBox.Show("Túl sok vagy túl kevés a fők száma!");
                return;
            }
            bool kedvezmenyes = MainWindow.ugyfelek.Where(x => x.Azon == ugyfelId.SelectedItem.ToString()).First().Vip;
            ar.Text = kedvezmenyes ? MainWindow.szobak[Convert.ToInt32(szobaId.SelectedItem) - 1].Kedvezmenyes(Convert.ToInt32(fo.Text), napok).ToString() : MainWindow.szobak[Convert.ToInt32(szobaId.SelectedItem) - 1].Ar(Convert.ToInt32(fo.Text), napok).ToString();
            ar.Text += " Ft";
        }

        private void ugyfelId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            szamitAr();
        }

        private void szobaId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            szamitAr();
        }

        private void vegrehajtas_Click(object sender, RoutedEventArgs e)
        {
            if (muvelet.SelectedItem == "Törlés")
            {
                MainWindow.foglalasok.RemoveAt(foglalasSorszam.SelectedIndex);
                foglalasSorszam.SelectedIndex = -1;
            }else if (szobaId.SelectedIndex == -1 || ugyfelId.SelectedIndex == -1 || erkezes.SelectedDate == null || tavozas.SelectedDate == null || fo.Text == "" || allapot.SelectedIndex == -1)
            {
                //MessageBox.Show("Hiányzó adat!");
                return;
            }
            int index = foglalasSorszam.SelectedIndex;


            int napok = (int)Convert.ToDateTime(tavozas.SelectedDate).Subtract(Convert.ToDateTime(erkezes.SelectedDate)).TotalDays;

            if (napok < 1)
            {
                MessageBox.Show("Hibás dátumok!");
                return;
            }

            if (muvelet.SelectedItem == "Módosítás")
            {
                if (index == -1)
                {
                    MessageBox.Show("Nincs kiválasztott foglalás!");
                    return;
                }

                MainWindow.foglalasok[index].Szobaszam = (int)szobaId.SelectedItem;
                MainWindow.foglalasok[index].UgyfelAzon = ugyfelId.SelectedItem.ToString();
                MainWindow.foglalasok[index].Erkezes = MainWindow.Datumba(erkezes.SelectedDate.ToString().Substring(0, 12));
                MainWindow.foglalasok[index].Tavozas = MainWindow.Datumba(tavozas.SelectedDate.ToString().Substring(0, 12));
                MainWindow.foglalasok[index].Fo = Convert.ToInt32(fo.Text);
                MainWindow.foglalasok[index].Ar = Convert.ToInt32(ar.Text.Substring(0,ar.Text.Length - 3));
                MainWindow.foglalasok[index].Allapot = allapot.SelectedItem.ToString();
            }
            else if (muvelet.SelectedItem == "Hozzáadás")
            {
                MainWindow.foglalasok.Add(new Foglalas(Convert.ToInt32(szobaId.SelectedItem), ugyfelId.SelectedItem.ToString(), MainWindow.Datumba(erkezes.SelectedDate.ToString().Substring(0, 12)), MainWindow.Datumba(tavozas.SelectedDate.ToString().Substring(0, 12)), Convert.ToInt32(fo.Text), Convert.ToInt32(ar.Text.Substring(0, ar.Text.Length - 3)), allapot.SelectedItem.ToString()));
            }
            MainWindow.KiirFoglalasok();
            atugor = true;
            frissitBoxok();
            MessageBox.Show("Sikeres művelet!");
        }
        private void frissitBoxok()
        {
            foglalasSorszam.Items.Clear();
            for (int i = 0; i < MainWindow.foglalasok.Count; i++)
            {
                foglalasSorszam.Items.Add(i + 1);
            }
            szobaId.SelectedIndex = -1;
            ugyfelId.SelectedIndex = -1;

            erkezes.SelectedDate = null;
            tavozas.SelectedDate = null;

            fo.Text = "";
            ar.Text = "";

            allapot.SelectedIndex = -1;
        }

        private void erkezes_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int napok = (int)Convert.ToDateTime(tavozas.SelectedDate).Subtract(Convert.ToDateTime(erkezes.SelectedDate)).TotalDays;

            if (napok < 1)
            {
                //MessageBox.Show("Az érkezésnek hamarabb kell lennie mint a távozásnak!");
                return;
            }
        }
        private void tavozas_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int napok = (int)Convert.ToDateTime(tavozas.SelectedDate).Subtract(Convert.ToDateTime(erkezes.SelectedDate)).TotalDays;

            if (napok < 1)
            {
                //MessageBox.Show("Az érkezésnek hamarabb kell lennie mint a távozásnak!");
                return;
            }
        }
    }
}