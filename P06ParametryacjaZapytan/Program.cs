using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06ParametryacjaZapytan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj imie");
            string imie = Console.ReadLine();

            SqlConnection connection; // nazwiazywanie polaczenia z baza 
            SqlCommand command; // przechowwanie polecen sql 
            SqlDataReader sqlDataReader; // czytanie wynikow z bazy 

            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=A_Zawodnicy;Integrated Security=True";

            connection = new SqlConnection(connectionString);

            // wpisanie do konsoli 
            // '; DROP table zawodnicy; -- 
           // command = new SqlCommand($"select * from zawodnicy where imie = '{imie}'", connection);

            string bezpiecznySQL = "select * from zawodnicy where imie = @imie";
            command = new SqlCommand(bezpiecznySQL, connection);
            command.Parameters.AddWithValue("@imie", imie);


            connection.Open();

            sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                string wynik = (string)sqlDataReader.GetValue(2) + " " + (string)sqlDataReader.GetValue(3);
                Console.WriteLine(wynik);
            }
            connection.Close();
            Console.ReadKey();


        }
    }
}
