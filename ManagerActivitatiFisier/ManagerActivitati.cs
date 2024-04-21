using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obiect;

namespace ManagerDate
{
    public class ManagerActivitati
    {
        private const int NR_MAX_Activitati = 100;

        // Lista in care se salveaza activitatile
        private Activitate[] activitati;
        private int nrActivitati;

        // Constructor
        public ManagerActivitati()
        {
            activitati = new Activitate[NR_MAX_Activitati];
            nrActivitati = 0;
        }

        // Adauga o activitate in lista
        public void AdaugaActivitate(Activitate activitate)
        {
            activitati[nrActivitati] = activitate;
            nrActivitati++;
        }

        // Sterge o activitate din lista
        /*public void StergeActivitate(Activitate element)
        {
            
        }*/

        // Returneaza toate activitatile din lista
        public Activitate[] GetActivitati(out int nrActivitati)
        {
            nrActivitati = this.nrActivitati;
            return activitati;
        }

        //Functia de cautare
        public Activitate[] CautaActivitati(string nume, string tip, DateTime data)
        {
            List<Activitate> rezultate = new List<Activitate>();
            for(int i = 0; i<nrActivitati; i++)
            {
                bool matchNume = string.IsNullOrEmpty(nume) || activitati[i].Nume.Equals(nume, StringComparison.OrdinalIgnoreCase);
                bool matchTip = string.IsNullOrEmpty(tip) || Enum.IsDefined( typeof(Activitate.TipActivitate),activitati[i].Tip);
                bool matchData = data == DateTime.MinValue || activitati[i].Data.Date == data.Date;

                if (matchNume && matchTip && matchData)
                {
                    rezultate.Add(activitati[i]);
                }
            }

            return rezultate.ToArray();
        }
    }
}
