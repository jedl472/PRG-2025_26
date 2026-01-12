using System;

namespace uloha_cisty_kod
{
  class Program
  {
    static void Main(string[] args)
    {
    

    }
    
    class Kinosal {
      private bool[,] _db { get; set; }
      private int rady { get; set; }
      private int sloupce { get; set; }

      public Kinosal(int rady, int sloupce) {
        // konstruktor ulozi do _db bool[v`elikostX, velikostY] a vyplni ji hodnotami false (vyrobi kinosal o dane velikosti a vyplni ho nezarezervovanimy misty)
        _db = new bool[rady,sloupce];

        Array.Fill(_db, false); 
      }

      public void Print() {
        // vytiskne do konzole promenou _db - stav rezervaci kinosalu
        for (int x = 0; x < length; x++)
        {
           for (int y = 0; y < length; y++)
           {
               Console.Writeln(_db[x, y]);
           }
        }
      }
    }

  }
}
