using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    internal class ManagerActivitati
    {
        // Lista in care se salveaza activitatile
        private List<Activitate> activitati;

        // Constructor
        public ManagerActivitati()
        {
            activitati = new List<Activitate>();
        }

        // Adauga o activitate in lista
        public void AdaugaActivitate(Activitate element)
        {
            activitati.Add(element);
        }

        // Sterge o activitate din lista
        public void StergeActivitate(Activitate element)
        {
            activitati.Remove(element);
        }

        // Afiseaza toate activitatile din lista
        public void AfiseazaActivitati()
        {
            if (activitati.Count == 0)
            {
                Console.WriteLine("Nu exista activitati.");
                return;
            }

            Console.WriteLine("Activitati:");
            foreach (Activitate element in activitati)
            {
                Console.WriteLine(element.Detalii());
                Console.WriteLine();
            }
        }
    }
}
