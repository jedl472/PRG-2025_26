using System.ComponentModel;

namespace uloha_0_zaklady 
{
  internal class Program
  {
    static T consoleReadType<T> (string query) {
      Console.Write(query);

      string? str = Console.ReadLine();

      TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

      while (converter.IsValid(str) == false | str == null)
      {
        Console.Write("Neplatny vstup. " + query);
        str = Console.ReadLine();
      }

      return (T)converter.ConvertFromString(str);
    }

    class Student {
      public string jmeno;
      public uint vek;
      public float prumernaZnamka;
      
      public Student () {
        jmeno = consoleReadType<string>("jmeno: ");
        vek = consoleReadType<uint>("vek  : ");
        prumernaZnamka = consoleReadType<float>("prumerna znamka: ");
      }

      public void consolePrint() {
        Console.WriteLine($"{jmeno}({vek}): {prumernaZnamka}");
      }
    }



    static void Main(string[] args) 
    {
      int pocetStudentu = consoleReadType<int>("Zadejte pocet studentu: ");
      
      List<Student> studenti = new List<Student>();
      for (int i = 0; i < pocetStudentu; i++)
      {
          Console.WriteLine("------ student {0} -------", i);
          studenti.Add(new Student());
      }

      while (true) {
        Console.WriteLine();
        char userInput = consoleReadType<char>("prikaz: ");
        
        switch(userInput) {
          case 'a':
            foreach(Student student in studenti) {
              student.consolePrint();
            }
            break;
          case 'b':
            foreach(Student student in studenti) {
              if (student.prumernaZnamka < 2.0) {
                student.consolePrint();
              } 
            }
            break;
          case 'c':
            uint soucetVeku = 0;
            foreach(Student student in studenti) {
              soucetVeku += student.vek;
            }
            Console.WriteLine("Prumerny vek: {0}", (float)soucetVeku/(float)pocetStudentu);
            break;
          case 'd':
            goto Gin;
        }
      }

      Gin:
      Console.WriteLine("fin");
    }
  }
}

