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
        //Laborator 5
        public enum TipActivitate
        {
            MUNCA = 1,
            SPORT = 2,
            EDUCATIE = 3,
            PETRECERE = 4,
            NECUNOSCUT = 0
        }

        public enum Prioritate
        {
            MIC = 1,
            MEDIU = 2,
            MARE = 3,
            NECUNOSCUT = 0
        }

        public enum Stare
        {
            IN_CURS = 1,
            TERMINATA = 2,
            NECUNOSCUT = 0
        }

        [Flags]
        public enum ZileSaptamana
        {
            Necunoscut = 0b_0000_0000,
            Luni = 0b_0000_0001,
            Marti = 0b_0000_0010,
            Miercuri = 0b_0000_0100,
            Joi = 0b_0000_0100,
            Vineri = 0b_0001_0000,
            Sambata = 0b_0010_0000,
            Duminica = 0b_0100_0000,
            Weekend = Sambata | Duminica
        }

        [Flags]
        public enum LunaAn
        {
            Necunoscut = 0b_0000_0000,
            Ianuarie = 0b_0000_0001,
            Februarie = 0b_0000_0010,
            Martie = 0b_0000_0100,
            Aprilie = 0b_0000_1000,
            Mai = 0b_0001_0000,
            Iunie = 0b_0010_0000,
            Iulie = 0b_0100_0000,
            August = 0b_1000_0000,
            Septembrie = 0b_0000_0001_0000,
            Octombrie = 0b_0000_0010_0000,
            Noiembrie = 0b_0000_0100_0000,
            Decembrie = 0b_0000_1000_0000
        }
        //Sfarsit laborator 5

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
