using System.Numerics;
using System.Runtime.InteropServices;

namespace prg_test_1
{
    internal class Program
    {
        static double distance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        static bool checkIfTriangle(int x1, int y1, int x2, int y2, int x3, int y3) 
        {  // Toto by melo zkontrolovat, jestli jsou ty 3 body linearne zavisle, ale nefunggugje to.
            if (x1 / x2 == y1 / y2)
            {
                return false;
            }

            if (x1 / x3 == y1 / y3)
            {
                return false;
            }

            if (x2 / x3 == y2 / y3)
            {
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            int[] data = new int[6];
            uint data_counter = 0;
            while (true)
            {
                if (data_counter == 6)
                {
                    data_counter = 0;

                    //if (checkIfTriangle(data[0], data[1], data[2], data[3], data[4], data[5]))
                    if(true)
                    {
                        Console.WriteLine(distance(data[0], data[1], data[2], data[3]));
                        Console.WriteLine(distance(data[2], data[3], data[4], data[5]));
                        Console.WriteLine(distance(data[0], data[1], data[4], data[5]));
                    } else
                    {
                        Console.WriteLine("Tyto tři body netvoří trojúhelník.");
                    }
                    Console.WriteLine();
                }
                string input = Console.ReadLine();
                
                if(input == "q")
                {
                    break;
                } else
                {
                    data[data_counter++] = Int32.Parse(input);
                 
                }

            }
        }
    }
}
