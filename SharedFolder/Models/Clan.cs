using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFolder.Models
{
    public class Clan
    {
        public int IdClana { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string KontaktInformacije { get; set; }
        public string Clanarina { get; set; }
        public string KorisnickoIme { get; set; } 
        public string Lozinka { get; set; }  
    }
}
