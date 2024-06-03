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
using System.Threading.Tasks.Dataflow;
using System.Text.RegularExpressions;

namespace kikelet_panzio
{
    public partial class MainWindow : Window
    {
        internal static List<Szoba> szobak = new List<Szoba>();
        internal static List<Ugyfel> ugyfelek = new List<Ugyfel>();
        internal static List<Foglalas> foglalasok = new List<Foglalas>();
        public MainWindow()
        {
            InitializeComponent();
            ablak.NavigationService.Navigate(new Kezdooldal());
            Beolvas();
        }
        public static DateOnly Datumba(string datum)
        {
            int[] datumS = datum.Replace(".", "").Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            return new DateOnly(datumS[0], datumS[1], datumS[2]);
        }
        public static string Datumbol(DateOnly datum)
        {
            return datum.ToString();
        }
        public static void BeolvasSzobak()
        {
            string[] sorok = File.ReadAllLines("szobak.txt");
            foreach (var sor in sorok)
            {
                szobak.Add(new Szoba(sor));
            }
        }
        public static void BeolvasUgyfelek()
        {
            string[] sorok = File.ReadAllLines("ugyfelek.txt");
            foreach (var sor in sorok)
            {
                ugyfelek.Add(new Ugyfel(sor));
            }
        }
        public static void BeolvasFoglalasok()
        {
            string[] sorok = File.ReadAllLines("foglalasok.txt");
            foreach (var sor in sorok)
            {
                foglalasok.Add(new Foglalas(sor));
            }
        }
        public static void KiirSzobak()
        {
            File.WriteAllLines("szobak.txt", szobak.Select(x => $"{x.Szobaszam};{x.Ferohely};{x.Arperfo}"));
        }
        public static void KiirUgyfelek()
        {
            File.WriteAllLines("ugyfelek.txt", ugyfelek.Select(x => $"{x.Azon};{x.Nev};{x.SzulDatum};{x.Email};{x.Vip}"));
        }
        public static void KiirFoglalasok()
        {
            File.WriteAllLines("foglalasok.txt", foglalasok.Select(x => $"{x.Szobaszam};{x.UgyfelAzon};{x.Erkezes};{x.Tavozas};{x.Fo};{x.Ar};{x.Allapot}"));
        }
        public void Beolvas()
        {
            BeolvasSzobak();
            BeolvasUgyfelek();
            BeolvasFoglalasok();
        }
        private void ablak_ContentRendered(object sender, EventArgs e)
        {
            ablak.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
        private void kezdooldal_Click(object sender, RoutedEventArgs e)
        {
            ablak.NavigationService.Navigate(new Kezdooldal());
            ablak.NavigationService.RemoveBackEntry();
        }
        private void adminSzoba_Click(object sender, RoutedEventArgs e)
        {
            ablak.NavigationService.Navigate(new Szobak());
            ablak.NavigationService.RemoveBackEntry();
        }
        private void adminUgyfel_Click(object sender, RoutedEventArgs e)
        {
            ablak.NavigationService.Navigate(new Ugyfelek());
            ablak.NavigationService.RemoveBackEntry();
        }

        private void adminFoglalas_Click(object sender, RoutedEventArgs e)
        {
            ablak.NavigationService.Navigate(new Foglalasok());
            ablak.NavigationService.RemoveBackEntry();
        }
        private void adminStatisztika_Click(object sender, RoutedEventArgs e)
        {
            ablak.NavigationService.Navigate(new Statisztikak());
            ablak.NavigationService.RemoveBackEntry();
        }
        private void kilep_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static bool megfeleloEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}