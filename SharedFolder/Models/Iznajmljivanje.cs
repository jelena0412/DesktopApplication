using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFolder.Models
{
    public class Iznajmljivanje
    {
        public int IdIznajmljivanja { get; set; }
        public int ISBN { get; set; }  
        public int IdClana { get; set; }
        public string DatumIznajmljivanja { get; set; }
        public string DatumVracanja { get; set; }  
        public Knjiga Knjiga { get; set; }
        public Iznajmljivanje() { }
        public Iznajmljivanje(int _ISBN, int _IdClana, string _DatumIznajmljivanja, string _DatumVracanja)
        {
            this.ISBN = _ISBN;
            this.IdClana= _IdClana;
            this.DatumIznajmljivanja = _DatumIznajmljivanja;
            this.DatumVracanja= _DatumVracanja;
        }
    }
}
