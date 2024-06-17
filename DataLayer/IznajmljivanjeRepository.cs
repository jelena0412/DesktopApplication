using SharedFolder;
using SharedFolder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class IznajmljivanjeRepository
    {
        public List<Iznajmljivanje> GetAllIznajmljivanje()
        {

            SqlConnection sqlConnection = new SqlConnection(Constants.connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM Iznajmljivanja";

            SqlDataReader dataReader = command.ExecuteReader();
            List<Iznajmljivanje> listaIznajmljivanja = new List<Iznajmljivanje>();
            while (dataReader.Read())
            {
                Iznajmljivanje iznajmljivanje = new Iznajmljivanje();
                iznajmljivanje.IdIznajmljivanja = dataReader.GetInt32(0);
                iznajmljivanje.ISBN = dataReader.GetInt32(1); // Ispravljeno da koristi int umesto stringa
                iznajmljivanje.IdClana = dataReader.GetInt32(2);
                iznajmljivanje.DatumIznajmljivanja = dataReader.GetString(3);
                iznajmljivanje.DatumVracanja = dataReader.GetString(4);

                listaIznajmljivanja.Add(iznajmljivanje);
            }
            sqlConnection.Close();
            return listaIznajmljivanja;

        }
        public List<Iznajmljivanje> GetIznajmljeneKnjige(int trenutniClanID)
        {
            List<Iznajmljivanje> listaIznajmljivanja = new List<Iznajmljivanje>();
            SqlConnection sqlConnection = new SqlConnection(Constants.connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT K.ISBN, K.Naslov, I.IdIznajmljivanja, I.DatumIznajmljivanja, I.DatumVracanja " +
                           "FROM Knjige K " +
                           "JOIN Iznajmljivanja I ON K.ISBN = I.ISBN " +
                           "WHERE I.ClanID = @ClanID";
            command.Parameters.AddWithValue("@ClanID", trenutniClanID);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Iznajmljivanje iznajmljivanje = new Iznajmljivanje();
                iznajmljivanje.Knjiga = new Knjiga(dataReader.GetInt32(0), dataReader.GetString(1));
                iznajmljivanje.IdIznajmljivanja = dataReader.GetInt32(2);
                iznajmljivanje.DatumIznajmljivanja = dataReader.GetString(3);
                iznajmljivanje.DatumVracanja = dataReader.GetString(4);

                listaIznajmljivanja.Add(iznajmljivanje);
            }
            sqlConnection.Close();

            return listaIznajmljivanja;
        }
        public bool InsertIznajmljivanje(Iznajmljivanje iznajmljivanje)
        {
            //Ukoliko duplikat postoji (true) onda ce funkcija vratiti false kako bi se u prezentacionom sloju
            //izbacila poruka kako je ta knjiga vec iznajmljena
            if (ProveriDuplikat(iznajmljivanje))
            {
                return false;
            }
            using (SqlConnection sqlConnection = new SqlConnection(Constants.connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "INSERT INTO Iznajmljivanja (ISBN, ClanID, DatumIznajmljivanja, DatumVracanja) " +
                                         "VALUES (@ISBN, @ClanID, @DatumIznajmljivanja, @DatumVracanja)";

                sqlCommand.Parameters.AddWithValue("@ISBN", iznajmljivanje.ISBN);
                sqlCommand.Parameters.AddWithValue("@ClanID", iznajmljivanje.IdClana);
                sqlCommand.Parameters.AddWithValue("@DatumIznajmljivanja", iznajmljivanje.DatumIznajmljivanja);
                sqlCommand.Parameters.AddWithValue("@DatumVracanja", iznajmljivanje.DatumVracanja);
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
                return true;
            }
        }
        //Funkcija koja proverava da li je korisnik vec iznajmio tu knjigu. Ukoliko postoji vec pozajmica sa
        //tom knjigom, vratice true, u suprotnom ako nije inzajmio tu knjigu vratice false
        private bool ProveriDuplikat(Iznajmljivanje iznajmljivanje)
        {
            List<Iznajmljivanje> listaIznajmljivanja = new List<Iznajmljivanje>();
            listaIznajmljivanja = GetAllIznajmljivanje();

            foreach (Iznajmljivanje pozajmica in listaIznajmljivanja)
            {
                if (pozajmica.ISBN == iznajmljivanje.ISBN)
                {
                    return true;
                }
            }
            return false;
        }



        public int DeleteIznajmljivanje(int iznajmljivanjeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.connectionString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "DELETE FROM Iznajmljivanja WHERE IdIznajmljivanja=@IdIznajmljivanja";

                    sqlCommand.Parameters.AddWithValue("@IdIznajmljivanja", iznajmljivanjeId);

                    sqlConnection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška prilikom brisanja iznajmljivanja iz baze: {ex.Message}");
                    return 0;
                }
            }
        }
    }
}
