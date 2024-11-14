using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02PolaczenieZBaza
{
    internal class PolaczenieZBaza
    {
        private readonly string connectionString;

        public PolaczenieZBaza()
        {
            connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=A_Zawodnicy;Integrated Security=True"; 
        }

        public PolaczenieZBaza(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        public object[][] WykonajPolecenieSQL(string sql)
        {
            SqlConnection connection; // nazwiazywanie polaczenia z baza 
            SqlCommand command; // przechowwanie polecen sql 
            SqlDataReader sqlDataReader; // czytanie wynikow z bazy 

            
            connection = new SqlConnection(connectionString);
            command = new SqlCommand(sql, connection);
            connection.Open();
            sqlDataReader = command.ExecuteReader();

            int liczbaKolumn = sqlDataReader.FieldCount;
            List<object[]> listaWierszy = new List<object[]>();

            while (sqlDataReader.Read())
            {
                object[] komorki = new object[liczbaKolumn];
                for (int i = 0; i < liczbaKolumn; i++)
                    komorki[i] = sqlDataReader.GetValue(i);

                listaWierszy.Add(komorki);
            }

            connection.Close();
            return listaWierszy.ToArray();
        }

    }
}
