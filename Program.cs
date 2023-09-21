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

            Console.WriteLine($"versenyzok szama: {versenyzok.Count} fo");

            var nokcelba = versenyzok.Count(v => !v.Kategoria && v.TavSzazalek == 100);
            Console.WriteLine($"celba erkezo nok: {nokcelba} fo");

            Console.Write("kerem a sportolo nevet: ");
            string kerNev = Console.ReadLine().ToLower();
            var kerVer = versenyzok.SingleOrDefault(v => v.Nev.ToLower() == kerNev);
            Console.WriteLine($"\tindult egyeniben: {(kerVer is not null ? "IGEN" : "NEM")}");
            if (kerVer is not null)
                Console.WriteLine($"\tteljesitette a tavot? {(kerVer.TavSzazalek == 100 ? "IGEN" : "NEM")}");


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
                .OrderBy(v => v.Versenyido)
                .First(v => v.Kategoria == kategoria && v.TavSzazalek == 100);

            return $"{(kategoria ? "Ferfiak:" : "Nok:")} " +
                $"{gyoztes.Nev} " +
                $"({gyoztes.Rajtszam}.) - " +
                $"{gyoztes.Versenyido}";
        }
    }
}
