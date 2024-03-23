using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Agenda
{
    class Activitate
    {
        public string Nume { get; set; }
        public DateTime Data { get; set; }
        public string Descriere { get; set; }
        public string Tip { get; set; }
        public int IdActivitate { get; set; }

        // Constructor 1
        public Activitate() 
        {
            Nume = Descriere = Tip = string.Empty;
            Data = DateTime.MinValue;
        }

        // Constructor 2
        public Activitate(string _nume, string _tip, DateTime _data, string _descriere)
        {
            Nume = _nume;
            Data = _data;
            Descriere = _descriere;
            Tip = _tip;
        }
        public string Detalii()
        {
            string detalii = $"Activitate: {Nume ?? " NECUNOSCUT "}\n" +
                $"Tipul: {Tip ?? " NECUNOSCUT "}\n" +
                $"Ziua si timpul: {Data}\n" +
                $"Descrierea: {Descriere ?? " NECUNOSCUT "}\n";

            return detalii;
        }

    }
}
