/*
 Uspořádání abecedy vždy jednoznačné nebude, protože existují vstupy typu: "xyz xz", kde nemáme žádnou informaci o úrovni 'x';

 Příklad vstupu s mnoha (dvěmi - stejně jako počet permutací počtu znaků) uspořádáními: "xy xy"

 Příklad vstupu s žádným možným uspořádáním "xy xx yx" (obsahuje cyklus)

*/

using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace abecedni_poradi
{
    internal class Program
    {

        static char? GraphFindEmptyKey(Dictionary<char, List<char>> graf)
        {
            foreach (var kvp in graf)
            {
                if (kvp.Value.Count == 0)
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        static void GraphPurgeValue(Dictionary<char, List<char>> graf, char valueToPurge)
        {
            foreach (var kvp in graf)
            {
                var newList = new List<char>();
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (kvp.Value[i] != valueToPurge)
                    {
                        newList.Add(kvp.Value[i]);
                    }
                }
                graf[kvp.Key] = newList;
            }
        }

        static void ZpracujVstupDoGrafu(out Dictionary<char, List<char>> out_dict)
        {
            out_dict = new Dictionary<char, List<char>>();

            string line = Console.ReadLine();

            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if (!out_dict.ContainsKey(words[i][j]))
                    {
                        out_dict.Add(words[i][j], new List<char>());
                    }
                }
            }

            for (int i = 0; i < words.Length - 1; i++)
            {
                int searchLevel = 0;
                while (words[i + 1][searchLevel] == words[i][searchLevel])
                {
                    if (searchLevel+1 != words[i].Length)
                    {
                        searchLevel++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (words[i + 1][searchLevel] != words[i][searchLevel]) { out_dict[words[i][searchLevel]].Add(words[i + 1][searchLevel]); } 
            }

        }

        static bool GrafNaPoradi(Dictionary<char, List<char>> graf, out List<char> out_list)  // Kahnův algoritmus
        {
            out_list = new List<char>();



            char? currentKey;

            while (graf.Count != 0)
            {
                currentKey = GraphFindEmptyKey(graf);
                if (currentKey == null) { return false; }

                out_list.Add((char)currentKey);

                GraphPurgeValue(graf, (char)currentKey);

                graf.Remove((char)currentKey);
            }

            return true;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Dictionary<char, List<char>> vstup;
                ZpracujVstupDoGrafu(out vstup);

                List<char> poradi = new List<char>();

                if (!GrafNaPoradi(vstup, out poradi))
                {
                    Console.WriteLine("obsahuje cyklus");
                }
                else
                {
                    for (int i = poradi.Count - 1; i >= 0; i--)
                    {
                        Console.Write(poradi[i]);
                        if (i != 0) Console.Write(" -> ");
                    }
                }

                Console.WriteLine("\n");
            }
        }
    }
}