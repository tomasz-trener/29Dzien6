﻿using P04Zawodnicy.Shared.Domains;
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
    public class ManagerZawodnikow
    {
        PolaczenieZBaza pzb = new PolaczenieZBaza();
        public List<Zawodnik> WczytajZawodnikow()
        {
            object[][] dane = pzb.WykonajPolecenieSQL("select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy order by nazwisko");

            List<Zawodnik> zawodnicy = mapujZawodnikow(dane);

            return zawodnicy;
        }

        private List<Zawodnik> mapujZawodnikow(object[][] dane)
        {
            List<Zawodnik> zawodnicy = new List<Zawodnik>();
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

                if (w[5] != DBNull.Value)
                    z.DataUrodzenia = (DateTime)w[5];

                if (w[6] != DBNull.Value)
                    z.Wzrost = (int)w[6];

                if (w[7] != DBNull.Value)
                    z.Waga = (int)w[7];

                zawodnicy.Add(z);
            }

            return zawodnicy;
        }

        public string[] PodajKraje()
        {
            object[][] dane = pzb.WykonajPolecenieSQL("select distinct kraj from zawodnicy");

            string[] kraje = new string[dane.Length];
            for (int i = 0; i < dane.Length; i++)
                kraje[i] = (string)dane[i][0];

            return kraje;
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            object[][] dane = pzb.WykonajPolecenieSQL($"select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy where kraj = '{kraj}' order by nazwisko");

            return mapujZawodnikow(dane).ToArray();
        }

   

        public double PodajSredniWzrost(string kraj)
        {
            object[][] dane = pzb.WykonajPolecenieSQL($"select avg(wzrost) from zawodnicy where kraj = '{kraj}'");

            return dane[0][0] == DBNull.Value ? double.NaN : Convert.ToDouble(dane[0][0]);
        }

       
   

        public void Usun(int id)
        {
            pzb.WykonajPolecenieSQL($"delete zawodnicy where id_zawodnika ={id}");
        }

        public void Dodaj(Zawodnik z)
        {
            string szablon = "insert into zawodnicy (id_trenera, imie, nazwisko, kraj, data_ur,wzrost,waga) values ({0},'{1}','{2}','{3}','{4}',{5},{6})";

            string sql = string.Format(szablon,
                z.Id_trenera == null ? "null" : z.Id_trenera.ToString(),
                 z.Imie, z.Nazwisko, z.Kraj, z.DataUrodzenia.ToString("yyyyMMdd"), z.Wzrost, z.Waga);

            pzb.WykonajPolecenieSQL(sql);
        }

        public void Edytuj(Zawodnik edytowany)
        {
            string id_trenera = edytowany.Id_trenera == null ? "null" : edytowany.Id_trenera.ToString();
            string sql = $@"update zawodnicy set 
	                        id_trenera = {id_trenera},
	                        imie = '{edytowany.Imie}',
	                        nazwisko = '{edytowany.Nazwisko}',
	                        kraj = '{edytowany.Kraj}',
	                        data_ur = '{edytowany.DataUrodzenia.ToString("yyyyMMdd")}',
	                        wzrost = {edytowany.Wzrost},
	                        waga ={edytowany.Waga}
	                        where id_zawodnika = {edytowany.Id_zawodnika}";

            pzb.WykonajPolecenieSQL(sql);
        }
    }
}

// komunikacja z bazą danych moze przebiegać na 3 sposoby; 

//1) Polecenia SQL 
//2) procedury wbudowane 
//3) ORM (object-relation-mapping)