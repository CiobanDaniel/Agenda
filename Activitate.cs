using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    internal class Activitate
    {
        public string Nume { get; set; }
        public DateTime Data { get; set; }
        public string Descriere { get; set; }

        // Constructor
        public Activitate(string _nume, DateTime _data, string _descriere)
        {
            Nume = _nume;
            Data = _data;
            Descriere = _descriere;
        }

        // Afisarea detaliilor activitatii
        public string Detalii()
        {
            string detalii = $"Activitate: {Nume}\nZiua si timpul: {Data}\nDescrierea: {Descriere}";
            return detalii;

        }
    }
}
