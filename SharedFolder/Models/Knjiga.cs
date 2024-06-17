using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFolder.Models
{
    public class Knjiga
    {
        public int ISBN { get; set; }  
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int GodinaIzdanja { get; set; }
        public Knjiga() { }
        public Knjiga(int ISBN, string naslov)
        {
            this.ISBN = ISBN;
            this.Naslov = naslov;
        }

    }
}
