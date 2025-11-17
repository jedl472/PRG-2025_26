using System;
using System.Numerics;
using System.ComponentModel;

namespace retizek_pratelstvi
{
    internal class Program
    {
        class SimpleGraph 
        {
            public int numberOfNodes;
            public List<List<int>> edges;
        }

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

        static void loadInput(out uint numberOfUsers, out List<(uint, uint)> interconnections) {
            interconnections = new List<(uint, uint)>(); 
            
            numberOfUsers = consoleReadType<uint> ("");
            
            string[] tuples = consoleReadType<string>("").Split(' ');

            foreach (string tuple in tuples) 
            {
                string[] splitted = tuple.Split('-');

                interconnections.Add((uint.Parse(splitted[0]), uint.Parse(splitted[1])));
            }
        }

        static void inputToGraph(out SimpleGraph graph, uint numberOfUsers, List<(uint, uint)> interconnections)) { //TODO predelat na spojaky
            graph.numberOfNodes = numberOfUsers;
            
            foreach (var i in interconnections) 
            {
                graph.edges[i.Item1].Add(i.Item2);
                graph.edges[i.Item2].Add(i.Item1);
            }
        }

        static void Main(string[] args)
        {
            uint numberOfUsers = 0;
            List<(uint, uint)> ic = new List<(uint, uint)>();

            loadInput(out numberOfUsers, out ic);

            Console.WriteLine(numberOfUsers);

            foreach (var i in ic) 
            {
                Console.WriteLine($"{i.Item1} - {i.Item2}");
            }
        }
    }
}


