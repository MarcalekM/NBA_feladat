using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NBA_feladat
{
    class Csapat
    {
        public string Nev {  get; set; }
        public string HazaiSzin { get; set; }
        public  string IdegenSzin { get; set; }
        public DateTime AlapitasiEv { get; set; }
        List<Jatekos> Jatekosok {  get; set; }
        List<Edzo> Edzok {  get; set; }

        public Csapat(string sor)
        {
            var temp = sor.Split(';');
            Nev = temp[0];
            HazaiSzin = temp[1];
            IdegenSzin = temp[2];
            AlapitasiEv = DateTime.Parse(temp[3]);
            Jatekosok = jatekosok.Where(j => j.Csapat.Equals(Nev)).ToList();
            Edzok = edzok.Where(j => j.Csapat.Equals(Nev)).ToList();
        }
    }
}
