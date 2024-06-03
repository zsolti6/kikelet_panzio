using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kikelet_panzio
{
    public partial class Statisztikak : Page
    {
        public Statisztikak()//nem tudom hogy hogyan mukodnek szallasokon a fizetesek
        {
            InitializeComponent();
            kiadottSzoba();
            visszajarok();
        }
        private void kezdo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            bevetelSzamitas();
        }
        private void veg_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            bevetelSzamitas();
        }
        private void bevetelSzamitas()
        {
            if (kezdo.SelectedDate != null && veg.SelectedDate != null && (int)Convert.ToDateTime(veg.SelectedDate).Subtract(Convert.ToDateTime(kezdo.SelectedDate)).TotalDays > 0)
            {
                int ossz = MainWindow.foglalasok.Where(x => x.Erkezes > MainWindow.Datumba(kezdo.SelectedDate.ToString().Substring(0, 12)) && x.Tavozas < MainWindow.Datumba(veg.SelectedDate.ToString().Substring(0, 12)) && x.Allapot == "teljesült").Sum(x => x.Ar);
                bevetel.Content = $"Összes bevétel -tól -ig: {ossz} Ft";
            }
            else
            {
                MessageBox.Show("Hiba!");
            }
        }
        private void kiadottSzoba()
        {
            Dictionary<int, int> szobak = new Dictionary<int, int>();//beleszamolom a lemondottakat is
            foreach (var foglalas in MainWindow.foglalasok)
            {
                if (!szobak.ContainsKey(foglalas.Szobaszam))
                {
                    szobak[foglalas.Szobaszam] = 1;
                }
                else
                {
                    szobak[foglalas.Szobaszam]++;
                }
            }
            var sorban = szobak.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            kiadott.Content = $"Legtöbbet kiadott szoba: {sorban.Last().Key}";
        }
        private void visszajarok()
        {
            Dictionary<string, int> ugyfelek = new Dictionary<string, int>();//ugyfelenkent mennyi foglalas volt (nem lemondott)
            foreach (var foglalas in MainWindow.foglalasok)
            {
                if (foglalas.Allapot != "lemondott")
                {
                    if (!ugyfelek.ContainsKey(foglalas.UgyfelAzon))
                    {
                        ugyfelek[foglalas.UgyfelAzon] = 1;
                    }
                    else
                    {
                        ugyfelek[foglalas.UgyfelAzon]++;
                    }
                }
            }
            string[] tobbmint1ugyfelek = ugyfelek.Where(x => x.Value > 1).Select(x => x.Key).ToArray();
            
            Dictionary<string, int> ugyfelSum = new Dictionary<string, int>();
            foreach (var foglalas in MainWindow.foglalasok)
            {
                if (foglalas.Allapot != "lemondott")
                {
                    if (!ugyfelSum.ContainsKey(foglalas.UgyfelAzon))
                    {
                        ugyfelSum[foglalas.UgyfelAzon] = foglalas.Ar;
                    }
                    else
                    {
                        ugyfelSum[foglalas.UgyfelAzon] += foglalas.Ar;
                    }
                }
            }
            var sorban = ugyfelSum.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Reverse();
            int i = 1;
            foreach (var ugyfel in sorban)
            {
                visszajaro.Items.Add($"{i}. {ugyfel.Key}: {ugyfel.Value} Ft");
                i++;
            }
        }
    }
}