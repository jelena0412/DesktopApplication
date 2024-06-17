using SharedFolder;
using SharedFolder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ClanRepository
    {
        public List<Clan> GetAllClanovi()
        {

            SqlConnection sqlConnection = new SqlConnection(Constants.connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM Clanovi";

            SqlDataReader dataReader = command.ExecuteReader();
            List<Clan> listaClanova = new List<Clan>();

            while (dataReader.Read())
            {
                Clan clan = new Clan();
                clan.IdClana = dataReader.GetInt32(0);
                clan.Ime = dataReader.GetString(1);
                clan.Prezime = dataReader.GetString(2);
                clan.Adresa = dataReader.GetString(3);
                clan.KontaktInformacije = dataReader.GetString(4);
                clan.Clanarina = dataReader.GetString(5);
                clan.KorisnickoIme = dataReader.GetString(6); // Dodano za korisničko ime
                clan.Lozinka = dataReader.GetString(7); // Dodano za lozinku

                listaClanova.Add(clan);
            }

            sqlConnection.Close();
        

            return listaClanova;
        }

        public int InsertClan(Clan clan)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.connectionString))
            {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "INSERT INTO Clanovi (Ime, Prezime, Adresa, KontaktInformacije,  Clanarina, KorisnickoIme, Lozinka) " +
                                             "VALUES (@Ime, @Prezime, @Adresa, @KontaktInformacije, @Clanarina, @KorisnickoIme, @Lozinka)";
                   
                    sqlCommand.Parameters.AddWithValue("@Ime", clan.Ime);
                    sqlCommand.Parameters.AddWithValue("@Prezime", clan.Prezime);
                    sqlCommand.Parameters.AddWithValue("@Adresa", clan.Adresa);
                    sqlCommand.Parameters.AddWithValue("@KontaktInformacije", clan.KontaktInformacije);
                    sqlCommand.Parameters.AddWithValue("@Clanarina", clan.Clanarina);
                    sqlCommand.Parameters.AddWithValue("@KorisnickoIme", clan.KorisnickoIme);
                    sqlCommand.Parameters.AddWithValue("@Lozinka", clan.Lozinka);

                    
                    return sqlCommand.ExecuteNonQuery();
                
            }
        }

        

        
    }
}

