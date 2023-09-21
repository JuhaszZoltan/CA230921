using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CA230921
{
    class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();

        static void Main()
        {
            // usign: IDisposable interface-re: automatikusan "lezárja" az olvasót
            using var sr = new StreamReader(
                @"..\..\..\src\ub2017egyeni.txt",
                Encoding.UTF8);
            // _ (discard): memóriába történő tárolás nélkül "eldobja" a sort.
            _ = sr.ReadLine();
            while (!sr.EndOfStream) versenyzok.Add(new Versenyzo(sr.ReadLine()));

            Console.WriteLine($"versenyzok szama: {versenyzok.Count}");

            //kódindentáció példány szintű hívási láncoknál:
            //selektor (a pont) a sor elején legyen!
            var ffatlag = versenyzok
                .Where(v => v.Kategoria && v.TavSzazalek == 100)
                .Average(v => v.IdoOraban);
            Console.WriteLine($"ferfiak atlagideje: {ffatlag:0.0000} ora");

            Console.WriteLine("verseny gyoztesei:");
            Console.WriteLine($"\t{Gyoztes(false)}");
            Console.WriteLine($"\t{Gyoztes(true)}");
        }
        static string Gyoztes(bool kategoria)
        {
            var gyoztes = versenyzok
                .Where(v => v.Kategoria == kategoria && v.TavSzazalek == 100)
                .OrderBy(v => v.Versenyido)
                .First();

            return $"{(kategoria ? "Ferfiak:" : "Nok:")} " +
                $"{gyoztes.Nev} " +
                $"({gyoztes.Rajtszam}.) - " +
                $"{gyoztes.Versenyido}";
        }
    }
}
