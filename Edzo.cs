using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_feladat
{
    class Edzo
    {
        public string Csapat {  get; set; }
        public string Nev {  get; set; }
        public DateTime SzuletesiEv { get; set; }

        public Edzo(string sor)
        {
            var temp = sor.Split(';');
            Csapat = temp[0];
            Nev = temp[1];
           SzuletesiEv = DateTime.Parse(temp[2]);
        }
    }
}
