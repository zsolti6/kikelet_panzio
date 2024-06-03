using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kikelet_panzio
{
    internal class Foglalas
    {
        int szobaszam;
        string ugyfelAzon;
        DateOnly erkezes;
        DateOnly tavozas;
        int fo;
        int ar;
        string allapot;
        public int Szobaszam { get => szobaszam; set => szobaszam = value; }
        public string UgyfelAzon { get => ugyfelAzon; set => ugyfelAzon = value; }
        public DateOnly Erkezes { get => erkezes; set => erkezes = value; }
        public DateOnly Tavozas { get => tavozas; set => tavozas = value; }
        public int Fo { get => fo; set => fo = value; }
        public int Ar { get => ar; set => ar = value; }
        public string Allapot { get => allapot; set => allapot = value; }
        public Foglalas(int szobaszam, string ugyfelAzon, DateOnly erkezes, DateOnly tavozas, int fo, int ar, string allapot)
        {
            this.szobaszam = szobaszam;
            this.ugyfelAzon = ugyfelAzon;
            this.erkezes = erkezes;
            this.tavozas = tavozas;
            this.fo = fo;
            this.ar = ar;
            this.allapot = allapot;
        }
        public Foglalas(string sor)
        {
            string[] adatok = sor.Split(';');
            szobaszam = Convert.ToInt32(adatok[0]);
            ugyfelAzon = adatok[1];
            erkezes = MainWindow.Datumba(adatok[2]);
            tavozas = MainWindow.Datumba(adatok[3]);
            fo = Convert.ToInt32(adatok[4]);
            ar = Convert.ToInt32(adatok[5]);
            allapot = adatok[6];
        }
    }
}