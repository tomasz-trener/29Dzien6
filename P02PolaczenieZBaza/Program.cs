using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02PolaczenieZBaza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PolaczenieZBaza pzb = new PolaczenieZBaza();
            object[][] wynik = pzb.WykonajPolecenieSQL("select * from zawodnicy");

            foreach (object[] wiersz in wynik)
                Console.WriteLine(string.Join(" ",wiersz));


            Console.ReadLine();
        }
    }
}
