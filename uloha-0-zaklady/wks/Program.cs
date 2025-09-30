namespace uloha_0_zaklady_vs
{
    internal class Program
    {
          static int read()
          {
              int output;

              String str = Console.ReadLine();

              while (int.TryParse(str, out output) == false)
              {
                  Console.WriteLine("Neplatny vstup");
                  str = Console.ReadLine();
              }

              return output;
          } 

          static void Main(string[] args)
          {
              Console.WriteLine("Zadejte pocet studentu: ");
              int pocetStudentu = read();

              Console.WriteLine("Toto je pocet studentu: ");
              Console.WriteLine(pocetStudentu);
          }
      }
}
