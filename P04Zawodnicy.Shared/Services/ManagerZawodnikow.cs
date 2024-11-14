using P04Zawodnicy.Shared.Domains;
using P04Zawodnicy.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P04Zawodnicy.Shared.Services
{
    // ten bedzie dzialac na bazie danych 
    internal class ManagerZawodnikow
    {
        PolaczenieZBaza pzb = new PolaczenieZBaza();
        public List<Zawodnik> WczytajZawodnikow()
        {
            object[][] dane = pzb.WykonajPolecenieSQL("select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy");

            List<Zawodnik> zawodnicy= new List<Zawodnik>();
            for (int i = 0; i < dane.Length; i++)
            {
                object[] w = dane[i]; // i-ty wiersz 
                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = (int)w[0];
                if (w[1] != DBNull.Value)
                    z.Id_trenera = (int)w[1];

                z.Imie = (string)w[2];
                z.Nazwisko = (string)w[3];
                z.Kraj = (string)w[4];

                if(w[5] != DBNull.Value)
                    z.DataUrodzenia = (DateTime)w[5];

                if (w[6] != DBNull.Value)
                    z.Waga = (int)w[6];

                if (w[7] != DBNull.Value)
                    z.Wzrost = (int)w[7];

                zawodnicy.Add(z);
            }

            return zawodnicy; 
        }

        public string[] PodajKraje()
        {
            

        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            
        }

        private void posrotujZawodnikowPoNazwisku(List<Zawodnik> zawodnicy)
        {
            
        }

        public double PodajSredniWzrost(string kraj)
        {
            
        }

       
        public void Zapisz()
        {
             
        }

        public void Usun(int id)
        {
             
        }

        public void Dodaj(Zawodnik zawodnik)
        {
            
        }
    }
}
