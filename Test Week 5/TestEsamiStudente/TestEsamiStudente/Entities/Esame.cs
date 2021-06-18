using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEsamiStudente.Entities
{
    public class Esame
    {
        public int ID { get; set; }
        public String Nome { get; set; }
        public int CFU { get; set; }
        public DateTime DataEsame { get; set; }
        public String Votazione { get; set; }
        public String Esito { get; set; }
        public Studente Studente { get; set; }
    }
}
