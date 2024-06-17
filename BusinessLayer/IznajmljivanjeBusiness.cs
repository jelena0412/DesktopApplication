using DataLayer;
using SharedFolder;
using SharedFolder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace BusinessLayer
{
    public class IznajmljivanjeBusiness
    {
        private readonly IznajmljivanjeRepository iznajmljivanjeRepo;
     
        public IznajmljivanjeBusiness()
        {
            this.iznajmljivanjeRepo = new IznajmljivanjeRepository();
        }

        public int UzmiISBN(object selectedObject)
        {
            int index = selectedObject.ToString().IndexOf("ISBN: ");

            string remainingString = selectedObject.ToString().Substring(index + "ISBN: ".Length);
            int endIndex = remainingString.IndexOf(' ');

            string isbnNumber = remainingString.Substring(0, endIndex);
            return Convert.ToInt32(isbnNumber);
        }
        public List<Iznajmljivanje> GetAllIznajmljivanje()
        {
            return this.iznajmljivanjeRepo.GetAllIznajmljivanje();
        }
        public List<Iznajmljivanje> GetIznajmljeneKnjige(int trenutniClanID)
        {
            return this.iznajmljivanjeRepo.GetIznajmljeneKnjige(trenutniClanID);
        }

        public bool DeleteRental(int iznajmljivanjeId)
        {
            return this.iznajmljivanjeRepo.DeleteIznajmljivanje(iznajmljivanjeId) > 0;
        }
    }
}
