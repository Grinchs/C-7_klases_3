using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C_7_klases_3
{
    public class Skolēns
    {
        public string Vārds;
        public string Uzvārds;
        public string Pers_kods;

        public void Reģistrēt()
        {
            Console.Write("Ievadi vārdu: ");
            Vārds = Console.ReadLine();

            Console.Write("Ievadi uzvārdu: ");
            Uzvārds = Console.ReadLine();

            Console.Write("Ievadi personas kodu: ");
            Pers_kods = Console.ReadLine();
        }

        public void Izvadīt()
        {
            Console.WriteLine($"Vārds: {Vārds}");
            Console.WriteLine($"Uzvārds: {Uzvārds}");
            Console.WriteLine($"Personas kods: {Pers_kods}");

            int vecums = AprēķinātVecumu();
            Console.WriteLine($"Vecums: {vecums}");
        }

        public int AprēķinātVecumu()
        {
            int datums = int.Parse(Pers_kods.Substring(0, 2));
            int mēnesis = int.Parse(Pers_kods.Substring(2, 2));
            int gads = int.Parse(Pers_kods.Substring(4, 2));

            DateTime today = DateTime.Today;

            int pilnsGads;

            if (gads > today.Year)
            {
                pilnsGads = 1900 + gads;
            }
            else
            {
                pilnsGads = 2000 + gads;
            }

            int vecums = today.Year - pilnsGads;

            if (today.Month < mēnesis || (today.Month == mēnesis && today.Day < datums))
            {
                vecums--;
            }

            return vecums;
        }
    }
    public class Skolotājs
    {
        public string Vārds;
        public string Uzvārds;
        public double Alga;
        public void Reģistrēt()
        {
            Console.Write("Ievadi vārdu: ");
            Vārds = Console.ReadLine();

            Console.Write("Ievadi uzvārdu: ");
            Uzvārds = Console.ReadLine();

            Console.Write("Ievadi algu: ");
            Alga = double.Parse(Console.ReadLine());
        }
        public void Izvadīt()
        {
            Console.WriteLine($"Vārds: {Vārds}");
            Console.WriteLine($"Uzvārds: {Uzvārds}");
            Console.WriteLine($"Alga pirms nodokļiem (BRUTO): {Alga}");

            bool irNodokļuGramatīna;

            Console.Write("Ir nodokļu gramatīna? (true/false): ");
            bool.TryParse(Console.ReadLine(), out irNodokļuGramatīna);

            double RealaAlga = AlgaPēcNodokļiem(irNodokļuGramatīna);
            Console.WriteLine($"Alga pēc nodokļiem (NETO): {RealaAlga}");
        }
        public double AlgaPēcNodokļiem(bool irNodokļuGramatīna)
        {
            double RealaAlga = Alga;

            if (irNodokļuGramatīna)
            {
                return RealaAlga - (Alga * 0.20);
            }
            else
            {
                return RealaAlga - (Alga * 0.23);
            }
        }
    }
    public class Klase
    {
        public string Nosaukums;
        public int Skolēnu_skaits;
        public Skolēns[] Skolēni;
        public Skolotājs Klases_audzinātājs;

        public void Reģistrēt()
        {
            Console.Write("Nosaukums: ");
            Nosaukums = Console.ReadLine();
            Console.Write("Skolēnu skaits: ");
            Skolēnu_skaits = int.Parse(Console.ReadLine());
            Skolēni = new Skolēns[Skolēnu_skaits];

            for (int i = 0; i < Skolēni.Length; i++)
            {
                Skolēni[i] = new Skolēns();
                Skolēni[i].Reģistrēt();
            }

            Klases_audzinātājs = new Skolotājs();
            Console.WriteLine("Skolotājs");
            Console.WriteLine();
            Klases_audzinātājs.Reģistrēt();
        }
        public void Izvadīt()
        {
            Console.WriteLine($"Nosaukums: {Nosaukums}");
            Console.WriteLine($"Skolēnu skaits: {Skolēnu_skaits}");
            foreach (var skolēns in Skolēni)
            {
                skolēns.Izvadīt();
            }
            Klases_audzinātājs.Izvadīt();
        }
        public Skolēns VecākaisSkolēns()
        {
            int vecākaisVecums = 0;
            Skolēns vecākaisSkolēns = null;

            foreach (var skolēns in Skolēni)
            {
                int vecums = skolēns.AprēķinātVecumu();
                if (vecums > vecākaisVecums)
                {
                    vecākaisVecums = vecums;
                    vecākaisSkolēns = skolēns;
                }
            }

            return vecākaisSkolēns;
        }
        public void MeklētSkolēnu(string ko_meklēt)
        {
            bool irAtrasts = false;

            foreach (var skolēns in Skolēni)
            {
                if (skolēns.Vārds == ko_meklēt || skolēns.Uzvārds == ko_meklēt)
                {
                    irAtrasts = true;
                    skolēns.Izvadīt();
                }
            }

            if (!irAtrasts)
            {
                Console.WriteLine("Nav tāda skolēna!!!");
            }
        }
    }
    internal class Program
    {
        public static Klase meklētKlasei(Klase[] klases, string nosaukums)
        {
            for (int i = 0; i < klases.Length; i++)
            {
                if (klases[i] != null)
                {
                    if (klases[i].Nosaukums == nosaukums)
                    {
                        return klases[i];
                    }
                }
            }

            return null;
        }
        public static void Main()
        {
            Klase[] klases;                             
            Console.Write("Ievadu klašu skaitu: ");
            int skaits = int.Parse(Console.ReadLine());
            klases = new Klase[skaits];

            bool loopStop = false;
            while (!loopStop)
            {
                Console.Clear();
                Console.WriteLine("1 - Izveidot jaunu klasi");
                Console.WriteLine("2 - Izvadīt informāciju par klasēm");
                Console.WriteLine("3 - Atrast klasi");
                Console.WriteLine("4 - Atrast skolēnu");
                Console.WriteLine("5 - Atrast klasē vecāko skolēnu");
                Console.WriteLine("0 - Iziet");
                Console.Write("\nIzvēlies darbību: ");
                string darbība = Console.ReadLine();

                switch (darbība)
                {
                    case "0":
                        loopStop = true;
                        break;

                    case "1":

                        for (int i = 0; i < klases.Length; i++)
                        {
                            if (klases[i] == null)
                            {
                                klases[i] = new Klase();
                                klases[i].Reģistrēt();
                                break;
                            }
                        }
                        break;

                    case "2":
                        for (int i = 0; i < klases.Length; i++)
                        {
                            if (klases[i] != null)
                            {
                                klases[i].Izvadīt();
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("Ieraksi klases nosaukumu: ");
                        string nosaukums = Console.ReadLine();

                        Klase atrastāKlase = meklētKlasei(klases, nosaukums);
                        if (atrastāKlase != null)
                        {
                            atrastāKlase.Izvadīt();
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Klase netika atrasta.");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        Console.WriteLine("Ievadiet skolēna vārdu vai uzvārdu: ");
                        string koMeklēt = Console.ReadLine();
                        for (int i = 0; i < klases.Length; i++)
                        {
                            if (klases[i] != null)
                            {
                                klases[i].MeklētSkolēnu(koMeklēt);
                            }
                        }

                        Console.ReadKey();
                        break;

                    case "5":
                        for (int i = 0; i < klases.Length; i++)
                        {
                            if (klases[i] != null)
                            {
                                klases[i].VecākaisSkolēns().Izvadīt();
                                Console.WriteLine($"Šis skolēns atrodas {klases[i].Nosaukums} klasē.");
                            }
                        }

                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}