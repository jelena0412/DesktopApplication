using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public static class Member
    {
        public static string TrenutnoKorisnickoIme { get; private set; }

        public static string TrenutnaLozinka { get; private set; }

        public static int TrenutniClanID { get; private set; }

        public static void PostaviTrenutnogClana(string KorisnickoIme)
        {
            TrenutnoKorisnickoIme = KorisnickoIme;
        }

        public static void PostaviTrenutnogCLanaLozinka(string Lozinka)
        {
            TrenutnaLozinka = Lozinka;
        }

        public static void PostaviTrenutniClanId(int IdClana)
        {
            TrenutniClanID = IdClana;
        }
    }
}
