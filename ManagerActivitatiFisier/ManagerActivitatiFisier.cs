using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obiect;

namespace ManagerDate
{
    public class ManagerActivitatiFisier
    {
        private const int NR_MAX_ACTIVITATI = 100;
        private string numeFisier;

        public ManagerActivitatiFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void SalveazaActivitati(Activitate activitate)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(activitate.ConversieLaSir_PentruFisier());
            }
        }

        public Activitate[] GetActivitati(out int nrActivitati)
        {
            Activitate[] activitate = new Activitate[NR_MAX_ACTIVITATI];

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrActivitati = 0;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    activitate[nrActivitati++] = new Activitate(linieFisier);
                }
            }

            Array.Resize(ref activitate, nrActivitati);

            return activitate;
        }

        public void StergeActivitate(Activitate activitate)
        {
            string tempFile = Path.GetTempFileName();

            using (StreamReader reader = new StreamReader(numeFisier))
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                string linie;
                bool activitateStearsa = false;

                while ((linie = reader.ReadLine()) != null)
                {
                    if (!activitateStearsa && linie.Equals(activitate.ConversieLaSir_PentruFisier()))
                    {
                        activitateStearsa = true;
                        continue; // Sărim peste activitatea de șters
                    }

                    writer.WriteLine(linie);
                }
            }

            File.Delete(numeFisier);
            File.Move(tempFile, numeFisier);
        }
    }
}
