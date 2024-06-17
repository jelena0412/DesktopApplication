using DataLayer;
using SharedFolder.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLayer
{
    public class KnjigaBusiness
    {
        private readonly KnjigaRepository knjigaRepo;

        public KnjigaBusiness()
        {
            this.knjigaRepo = new KnjigaRepository();
        }

       

        public List<string> PretragaPoAutoru(string autor)
        {
            var knjigeZadatogAutora= knjigaRepo.GetAllKnjige()
            .Where(k => k.Autor.ToLower().Contains(autor.ToLower()))
            .ToList();

            if (knjigeZadatogAutora.Any())
            {
                return knjigeZadatogAutora
                    .Select(k => $"Autor: {k.Autor} Naslov: {k.Naslov}- ISBN: {k.ISBN} Godina izdanja: {k.GodinaIzdanja}")
                    .ToList();
            }
            else
            {
                return null;
            }
        }
        public List<string> PretragaPoNaslovu(string naslov)
        {
            var knjigeZadatogNaslova = knjigaRepo.GetAllKnjige()
            .Where(k => k.Naslov.ToLower().Contains(naslov.ToLower()))
            .ToList();

            if (knjigeZadatogNaslova.Any())
            {
                return knjigeZadatogNaslova
                    .Select(k => $"Autor: {k.Autor} Naslov: {k.Naslov}- ISBN: {k.ISBN} Godina izdanja: {k.GodinaIzdanja}")
                    .ToList();
            }
            else
            {
                return null;
            }
        }
        public Iznajmljivanje FormirajPozajmicu(string selectedObject, int trenutniClanID)
        {
            int index = selectedObject.ToString().IndexOf("ISBN: ");

            string remainingString = selectedObject.ToString().Substring(index + "ISBN: ".Length);
            int endIndex = remainingString.IndexOf(' ');

            string isbnNumber = remainingString.Substring(0, endIndex);

            string vremeIznajmljivanja = DateTime.Now.ToString();
            string vremeVracanja = DateTime.Now.AddDays(90).ToString();

            Iznajmljivanje pozajmica = new Iznajmljivanje(Convert.ToInt32(isbnNumber), trenutniClanID,
                vremeIznajmljivanja, vremeVracanja);

            return pozajmica;
        }
    }
}
