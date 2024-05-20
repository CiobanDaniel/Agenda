using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
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
            RECREERE = 4,
            NECUNOSCUT = 0
        }

        public enum TipPrioritate
        {
            Mica = 1,
            Medie = 2,
            Mare = 3,
            Necunoscuta = 0
        }

        [Flags]
        public enum TipOptiuni
        {
            Fara = 0,
            Notificare = 1,
            Repetare = 2,
            Alarma = 3
        };
        //Sfarsit laborator 5

        private static readonly string dateFormat = "yyyy-MM-ddTHH:mmZ";
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        //private const bool SUCCES = true;

        private const int NUME = 1;
        private const int TIP = 2;
        private const int DATA = 3;
        private const int DESCRIERE = 4;
        private const int PRIORITATE = 5;
        private const int OPTIUNI = 6;
        private const int IDACTIVITATE = 0;

        public string Nume { get; set; }
        public string Data { get; set; }
        public string Descriere { get; set; }
        public TipActivitate Tip { get; set; }
        public TipPrioritate Prioritate { get; set; }
        public string[] Optiuni { get; set; }
        public int IdActivitate { get; set; }

        // Constructor default
        public Activitate() 
        {
            Nume = Descriere = string.Empty;
            Tip = TipActivitate.NECUNOSCUT;
            Data = DateTime.Now.ToString();
            Descriere = string.Empty;
            Prioritate = TipPrioritate.Necunoscuta;
        }

        // Constructor citire de la tastatura
        public Activitate(string _nume, string _tip, string _data, string _descriere, string _prioritate, string[] _optiuni)
        {
            Nume = _nume;
            Data = _data;
            Descriere = _descriere;
            Enum.TryParse(_tip, out TipActivitate tip);
            Tip = tip;
            Optiuni = _optiuni;
            Enum.TryParse(_prioritate, out TipPrioritate prioritate);
            Prioritate = prioritate;
        }

        //Constructor citire din fisier
        public Activitate(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            this.IdActivitate = Convert.ToInt32(dateFisier[IDACTIVITATE]);
            this.Nume = dateFisier[NUME];
            this.Data = dateFisier[DATA];
            this.Descriere = dateFisier[DESCRIERE];
            Enum.TryParse(dateFisier[TIP], out TipActivitate tip);
            this.Tip = tip;
            Enum.TryParse(dateFisier[PRIORITATE], out TipPrioritate prioritate);
            this.Prioritate = prioritate;
            this.Optiuni = dateFisier[OPTIUNI].Split(',');
        }
        public string Detalii()
        {
            string detalii = $"Activitate: {Nume ?? " NECUNOSCUT "}\n" +
                $"Tipul: {Tip}\n" +
                $"Ziua si timpul: {$"{Data}" ?? "NECUNOSCUT"}\n" +
                $"Descrierea: {Descriere ?? " NECUNOSCUT "}\n";

            return detalii;
        }
        public string ConversieLaSir_PentruFisier()
        {
            string timp;
            DateTime result;

            if (DateTime.TryParse(Data, out result))
            {
                timp = result.ToString(/*dateFormat*/);
            }
            else
            {
                timp = DateTime.Now.ToString(/*dateFormat*/);
            }
            string optiuniString;
            if (Optiuni != null)
            {
                optiuniString = string.Join(",", Optiuni);
            }
            else
            {
                optiuniString = string.Empty;
            }
            string activitatePentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdActivitate.ToString(),
                (Nume ?? " NECUNOSCUT "),
                Enum.GetName(typeof(TipActivitate), Tip),
                timp,
                (Descriere ?? " NECUNOSCUT "),
                Enum.GetName(typeof(TipPrioritate), Prioritate),
                (optiuniString ?? " FARA "));

            return activitatePentruFisier;
        }
    }
}
