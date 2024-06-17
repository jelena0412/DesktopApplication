using SharedFolder.Models;
using SharedFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DataLayer
{
    public class KnjigaRepository
    {
        public List<Knjiga> GetAllKnjige()
        {
            SqlConnection sqlConnection = new SqlConnection(Constants.connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
             command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM Knjige";

                    SqlDataReader dataReader = command.ExecuteReader();
            List<Knjiga> listaKnjiga = new List<Knjiga>();

            while (dataReader.Read())
                    {
                        Knjiga knjiga = new Knjiga();
                        knjiga.ISBN = dataReader.GetInt32(0);
                        knjiga.Naslov = dataReader.GetString(1);
                        knjiga.Autor = dataReader.GetString(2);
                        knjiga.GodinaIzdanja = dataReader.GetInt32(3);
                       
                       

                        listaKnjiga.Add(knjiga);
                    }
            sqlConnection.Close();
                
           
            return listaKnjiga;
        }
        public void PrikaziKnjigeUDatagridView(DataGridView dataGridView)
        {
            List<Knjiga> knjige = GetAllKnjige();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ISBN", typeof(int));
            dataTable.Columns.Add("Naslov", typeof(string));
            dataTable.Columns.Add("Autor", typeof(string));
            dataTable.Columns.Add("Godina Izdanja", typeof(int));
           

            foreach (Knjiga knjiga in knjige)
            {
                dataTable.Rows.Add(knjiga.ISBN, knjiga.Naslov, knjiga.Autor, knjiga.GodinaIzdanja);
            }

            dataGridView.DataSource = dataTable;
        }
        public int InsertKnjiga(Knjiga knjiga)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Constants.connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "INSERT INTO Knjige (ISBN, Naslov, Autor, GodinaIzdanja,  Iznajmljena) " +
                                             "VALUES (@ISBN, @Naslov, @Autor, @GodinaIzdanja,  @Iznajmljena)";

                    sqlCommand.Parameters.AddWithValue("@ISBN", knjiga.ISBN);
                    sqlCommand.Parameters.AddWithValue("@Naslov", knjiga.Naslov);
                    sqlCommand.Parameters.AddWithValue("@Autor", knjiga.Autor);
                    sqlCommand.Parameters.AddWithValue("@GodinaIzdanja", knjiga.GodinaIzdanja);
                    

                    
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška prilikom dodavanja knjige u bazu: {ex.Message}");
                    return 0;
                }
            }
        }

        
        }

       
    }

