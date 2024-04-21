using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Obiect;
using ManagerDate;
using System.IO;

namespace Agenda
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string numeFisier = "Activitati.txt";
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            bool continua = true;

            Activitate activitatenoua = new Activitate();
            int nrActivitati = 0;
            int nrActivitatiFisier = 0;

            ManagerActivitatiFisier managerActivitatiFisier = new ManagerActivitatiFisier(caleCompletaFisier);
            ManagerActivitati managerActivitati = new ManagerActivitati();

            while (continua) 
            {
                Console.WriteLine("Lista optiunilor:\n" +
                    "C. Adauga o activitate.\n" +
                    "I. Afiseaza ultima activitate introdusa.\n" +
                    "A. Afiseaza activitatile din agenda (din memorie).\n" +
                    "D. Afiseaza activitatile din fisier.\n" +
                    "B. Cautare dupa anumite criterii in agenda (in memorie).\n" +
                    "S. Salveaza o activitate in agenda (in memorie).\n" +
                    "F. Salveaza o activitate in fisier.\n" +
                    "P. Informatii proiect.\n" +
                    "X. Iesire din program.\n");

                Console.WriteLine("Alege optiunea: ");

                string optiune = Console.ReadLine().ToLower();

                switch (optiune)
                {
                    case "c":
                        //Adauga o activitate noua in agenda
                        activitatenoua = CitireActivitateTastatura();
                        break;

                    case "i":
                        //Afiseaza ultima activitate introdusa
                        AfisareActivitate(activitatenoua);
                        break;

                    case "b":
                        Console.WriteLine("Introdu numele activitatii pentru cautare (lasa gol pentru a omite):");
                        string numeCautat = Console.ReadLine();

                        Console.WriteLine("Introdu tipul activitatii pentru cautare (lasa gol pentru a omite):");
                        string tipCautat = Console.ReadLine();

                        Console.WriteLine("Introdu data activitatii pentru cautare (lasa gol pentru a omite):");
                        DateTime dataCautata;
                        if (!DateTime.TryParse(Console.ReadLine(), out dataCautata))
                        {
                            dataCautata = DateTime.MinValue; // Default value if parsing fails
                        }

                        Activitate[] activitatiCautate = managerActivitati.CautaActivitati(numeCautat, tipCautat, dataCautata);
                        AfisareActivitatiCautate(activitatiCautate, activitatiCautate.Length);
                        break;

                    case "a":
                        //Afiseaza toata lista de activitati din memorie
                        Activitate[] activitati = managerActivitati.GetActivitati(out nrActivitati);
                        AfisareActivitati(activitati,nrActivitati);
                        break;

                    case "d":
                        //Afiseaza lista de activitati din fisier
                        Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out nrActivitatiFisier);
                        AfisareActivitati(activitatiFisier, nrActivitatiFisier);
                        break;

                    case "s":
                        //Salveaza o activitate in memorie
                        int idActivitate = nrActivitati + 1;
                        activitatenoua.IdActivitate = idActivitate;
                        //adaugare student in vectorul de obiecte
                        managerActivitati.AdaugaActivitate(activitatenoua);

                        break;

                    case "f":
                        //Salveaza o activitate in fisier
                        activitatenoua.IdActivitate = ++nrActivitatiFisier;
                        //adaugare student in fisier
                        managerActivitatiFisier.SalveazaActivitati(activitatenoua);

                        break;

                    case "p":
                        //Afiseaza informatii despre proiect
                        Console.WriteLine("Nume proiect: Agenda\n" +
                            "Nume student: Cioban Daniel\n" +
                            "Grupa student: 3121a\n" +
                            "Tema proiect: Aplicatie tip agenda in care se vor administra activitatile unei persoane pe o perioada de timp.\n");
                        break;

                    case "x":
                        //Opreste executia programului
                        continua = false;
                        break;

                    default:
                        //In caz ca utilizatorul a introdus optiune gresita
                        Console.WriteLine("Optiune gresita.");
                        break;
                }
            }


        }
        public static Activitate CitireActivitateTastatura()
        {
            DateTime data;

            Console.Write("Introdu numele activitatii:");
            string nume = Console.ReadLine();

            Console.Write("Introdu tipul activitatii:");
            string tip = Console.ReadLine();

            bool ok = false;

            do
            {
                Console.WriteLine("Introdu data si ora (In format aaaa.ll.zz oo:mm):");
                string datain = Console.ReadLine();
                if (DateTime.TryParse(datain, out data))
                {
                    ok = true;
                }
                else
                {
                    Console.WriteLine("Format gresit.");
                }
            } while (!ok);

            Console.Write("Introdu descrierea activitatii:");
            string descriere = Console.ReadLine();

            Activitate activitate = new Activitate(nume, tip, data, descriere);
            return activitate;
        }

        public static void AfisareActivitate(Activitate activitate)
        {
            string detalii = $"Activitate: {activitate.Nume ?? " NECUNOSCUT "}\n" +
                $"Tipul: {activitate.Tip}\n" +
                $"Ziua si timpul: {activitate.Data}\n" +
                $"Descrierea: {activitate.Descriere ?? " NECUNOSCUT "}\n";

            Console.WriteLine(detalii);
        }

        public static void AfisareActivitati(Activitate[] activitati, int nrActivitati)
        {
            Console.WriteLine("Activitatile sunt:");
            for (int contor = 0; contor < nrActivitati; contor++)
            {
                string detalii = activitati[contor].Detalii();
                Console.WriteLine(detalii);
            }
        }

        public static void AfisareActivitatiCautate(Activitate[] activitatiCautate, int nrActivitatiCautate)
        {
            if (activitatiCautate == null || activitatiCautate.Length == 0)
            {
                Console.WriteLine("Nu s-au gasit activitati conform criteriilor.");
                return;
            }
            Console.WriteLine("Activitatile sunt:");
            for (int contor = 0; contor < nrActivitatiCautate; contor++)
            {
                if (activitatiCautate[contor] != null)
                {
                    string detalii = activitatiCautate[contor].Detalii();
                    Console.WriteLine(detalii);
                }
                else
                {
                    Console.WriteLine("Activitatea este null.");
                }
            }
        }
    }
}
