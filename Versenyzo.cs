using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA230921
{
    class Versenyzo
    {
        public string Nev { get; set; }
        public string Rajtszam { get; set; }
        public bool Kategoria { get; set; }
        public TimeSpan Versenyido { get; set; }
        public int TavSzazalek { get; set; }
        public double IdoOraban => Versenyido.TotalHours;

        public Versenyzo(string r)
        {
            var v = r.Split(';');
            Nev = v[0];
            Rajtszam = v[1];
            Kategoria = v[2] == "Ferfi";
            var t = v[3].Split(':');
            Versenyido = new TimeSpan(
                int.Parse(t[0]),
                int.Parse(t[1]),
                int.Parse(t[2]));
            TavSzazalek = int.Parse(v[4]);
        }
    }
}
