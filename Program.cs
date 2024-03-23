using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    
    internal class Program
    {
        static void Main(string[] args)
        {

            bool continua = true;

            ManagerActivitati managerActivitati = new ManagerActivitati();

            while (continua)
            {
                Console.WriteLine("Lista optiunilor:\n" +
                    "A. Adauga o activitate.\n" +
                    "S. Sterge o activitate.\n" +
                    "D. Afiseaza lista cu activitati.\n" +
                    "I. Informatii proiect.\n" +
                    "X. Iesire din program.\n");

                

                Console.WriteLine("Alege optiunea: ");

                string optiune = Console.ReadLine().ToLower();

                switch (optiune)
                {
                    case "a":
                        //Adauga o activitate noua in agenda
                        DateTime data = new DateTime(2024,01,01,07,30,00);
                        Activitate activitate = new Activitate("Pranz", data, "");
                        managerActivitati.AdaugaActivitate(activitate);
                        break;
                    case "s":
                        //Sterge o activitate din agenda
                        break;
                    case "d":
                        //Afiseaza toata lista de activitati
                        managerActivitati.AfiseazaActivitati();
                        break;
                    case "i":
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
    }
}
