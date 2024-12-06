using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace NBA_feladat
{
    class Jatekos
    {
        public string Csapat {  get; set; }
        public string Mezszam {  get; set; }
        public string Nev {  get; set; }
        public string Poszt { get; set; }
    
        public Jatekos(string sor)
        {
            var temp = sor.Split(';');
            Csapat = temp[0];
            Mezszam = temp[1];
            Nev = temp[2];
            Poszt = temp[3];
        }
    
    }
}
