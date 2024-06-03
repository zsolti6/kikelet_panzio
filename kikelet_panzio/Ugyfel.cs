using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kikelet_panzio;

namespace kikelet_panzio
{
    internal class Ugyfel
    {
        string azon;
        string nev;
        DateOnly szulDatum;
        string email;
        bool vip;
        public string Azon { get => azon;}
        public string Nev { get => nev; set => nev = value; }
        public DateOnly SzulDatum { get => szulDatum; set => szulDatum = value; }
        public string Email { get => email; set => email = value; }
        public bool Vip { get => vip; set => vip = value; }
        public Ugyfel(string nev, DateOnly szulDatum, string email, bool vip)
        {
            this.azon = $"{nev}#{DateTime.Now}";
            this.nev = nev;
            this.szulDatum = szulDatum;
            this.email = email;
            this.vip = vip;
        }
        public Ugyfel(string sor)
        {
            string[] adatok = sor.Split(';');
            azon = adatok[0];
            nev = adatok[1];
            szulDatum = MainWindow.Datumba(adatok[2]);
            email = adatok[3];
            vip = adatok[4] == "True";
        }
    }
}
