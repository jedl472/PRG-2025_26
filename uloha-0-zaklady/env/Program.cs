using System.ComponentModel;

namespace uloha_0_zaklady 
{
  internal class Program
  {
    class ConsoleReader<inputType>
    {
      public inputType read(string query)
      {
        inputType output;

        Console.Write(query);
        Console.WriteLine();

        String str = Console.ReadLine();

        TypeConverter converter = TypeDescriptor.GetConverter(typeof(inputType));

        while (converter.IsValid(str) == false)
        {
          Console.WriteLine("Neplatny vstup");
          str = Console.ReadLine();
        }

        return (inputType)converter.ConvertFromString(str);
      }
    }  

    static void Main(string[] args) 
    {
      Console.WriteLine("test");
    }
  }
}

//static void consoleRead(string query, Type targetType, out targetType output) {
//  Console.write(query);
//  Console.writeLine();
//  String str = Console.ReadLine();
//
//  targetType output;
// 
//  while (inputType.TryParse(str, out output) == false)
//  {
//      Console.WriteLine("Neplatny vstup");
//      str = Console.ReadLine();
//  }
//}
