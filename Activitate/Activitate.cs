using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Obiect
{
    public class Activitate
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const bool SUCCES = true;

        private const int NUME = 0;
        private const int TIP = 1;
        private const int DATA = 2;
        private const int DESCRIERE = 3;
        private const int IDACTIVITATE = 4;

        public string Nume { get; set; }
        public DateTime Data { get; set; }
        public string Descriere { get; set; }
        public string Tip { get; set; }
        public int IdActivitate { get; set; }

        // Constructor default
        public Activitate() 
        {
            Nume = Descriere = Tip = string.Empty;
            Data = DateTime.MinValue;
        }

        // Constructor citire de la tastatura
        public Activitate(string _nume, string _tip, DateTime _data, string _descriere)
        {
            Nume = _nume;
            Data = _data;
            Descriere = _descriere;
            Tip = _tip;
        }

        //Constructor citire din fisier
        public Activitate(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            this.IdActivitate = Convert.ToInt32(dateFisier[IDACTIVITATE]);
            this.Nume = dateFisier[NUME];
            this.Data = DateTime.Parse(dateFisier[DATA]);
            this.Descriere = dateFisier[DESCRIERE];
            this.Tip = dateFisier[TIP];
        }
        public string Detalii()
        {
            string detalii = $"Activitate: {Nume ?? " NECUNOSCUT "}\n" +
                $"Tipul: {Tip ?? " NECUNOSCUT "}\n" +
                $"Ziua si timpul: {$"{Data}" ?? "NECUNOSCUT"}\n" +
                $"Descrierea: {Descriere ?? " NECUNOSCUT "}\n";

            return detalii;
        }
        public string ConversieLaSir_PentruFisier()
        {
            DateTime timp;
            if (DateTime.TryParse(Data.ToString(), out timp))
            {
                timp = Data;
            }
            else
            {
                timp = DateTime.MinValue;
            }
            string activitatePentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                SEPARATOR_PRINCIPAL_FISIER,
                (Nume ?? " NECUNOSCUT "),
                (Tip ?? " NECUNOSCUT "),
                timp.ToString(),
                (Descriere ?? " NECUNOSCUT "),
                IdActivitate.ToString());

            return activitatePentruFisier;
        }
    }
}
