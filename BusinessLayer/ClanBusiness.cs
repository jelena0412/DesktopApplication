using DataLayer;
using System;
using SharedFolder.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class ClanBusiness
    {
        private readonly ClanRepository clanRepo;

        public ClanBusiness()
        {
            this.clanRepo = new ClanRepository();
        }

        //Funkcija koja iz liste clanova proverava unetog korisnika i ukoliko je prijava uspesna
        //onda funkcija vraca true da je uspela i vraca id clana koji se kasnije smesta u klasu Member
        public bool Prijava(string KorisnickoIme, string Lozinka, ref int idClana)
        {
            List<Clan> lista = this.clanRepo.GetAllClanovi();
            foreach (Clan clan in lista)
            {
                if (clan.KorisnickoIme == KorisnickoIme && clan.Lozinka == Lozinka)
                {
                    //Ukoliko pronadje korisnika onda postavlja id clana i zavrsava funkciju sa true
                    //jer nije potrebno da se dalje vrsi provera
                    idClana = clan.IdClana;
                    return true;
                }
            }
            //idClana je ovde postavljen zato sto program u suprotnom izbacuje gresku da funkcija ne
            //moze da se zavrsi dok se ne osigura da je idClana postavljen bilo gde u kodu jer program
            //misli da se idClana nece u svim slucajevima postaviti na neku vrednost (jer se vrednost psotavlja
            //samo u if-u i nece se uvek izvrsiti)
            idClana = 0;
            return false;
        }
        public List<Clan> GetAllMembers()
        {
            return this.clanRepo.GetAllClanovi();
        }

        public bool InsertMember(Clan clan)
        {
            return this.clanRepo.InsertClan(clan) > 0;
        }

       
    }
}

