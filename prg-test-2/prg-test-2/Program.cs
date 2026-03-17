using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace prg_test_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            int bfsStart;
            int bfsEnd;
            testovaUlohaLoad(graph, out bfsStart, out bfsEnd);

            var bfsOut = graph.BFS(bfsStart, bfsEnd);

            Console.WriteLine();

            if (bfsOut == null)
            {
                Console.WriteLine("neexistuje");
            } else
            {
                Console.Write(bfsStart);
                for (int i = bfsOut.Count - 1; i >= 0; i--) //otoci list z bfs
                {
                    Console.Write(" ");
                    Console.Write(bfsOut[i]);
                }
            }
        }

        // nacte data podle zadani ulohy
        static void testovaUlohaLoad(Graph graph, out int start, out int end)
        {
            graph.nodes = new Dictionary<int, List<int>>();
            graph.numberOfNodes = Convert.ToInt32(Console.ReadLine());

            //TODO když graf nemá hrany, tak tato funkce chcípne
            do
            {
                string[] line = Console.ReadLine().Split();

                if (line.Length != 2)
                {
                    start = Convert.ToInt32(line[0]);
                    break;
                }

                int a = Convert.ToInt32(line[0]);
                int b = Convert.ToInt32(line[1]);

                if (!graph.nodes.ContainsKey(a)) graph.nodes.Add(a, new List<int>());
                if (!graph.nodes.ContainsKey(b)) graph.nodes.Add(b, new List<int>());

                if (!graph.nodes[a].Contains(b)) graph.nodes[a].Add(b);
                if (!graph.nodes[b].Contains(a)) graph.nodes[b].Add(a);
            } while (true);

            end = Convert.ToInt32(Console.ReadLine());
        }

        class Graph
        {
            public int numberOfNodes;
            public Dictionary<int, List<int>> nodes;

            public void Print()
            {
                foreach (var kvp in nodes)
                {
                    Console.Write(kvp.Key);
                    Console.Write(": ");

                    foreach (int x in kvp.Value)
                    {
                        Console.Write(x);
                        Console.Write(" ");
                    }

                    Console.Write('\n');
                }
            }

            //vrati sousedni nody. Samostatna funkce, protoze v pripade matice sousednosti by byla slozitejsi.
            public List<int> AdjacementNodes(int nodeToSearch)
            {
                List<int> adjacementNodes = nodes[nodeToSearch];

                return adjacementNodes;
            }

            public List<int> BFS(int start, int end)
            {
                Queue<int> queue = new Queue<int>();
                List<int> closedNodes = new List<int>();

                // pro ucely backtracku. vytvori mapu k jakemu nodu se bfs dostalo odkud
                List<int?> visitedInts = new List<int?>();
                for (int i = 0; i <= numberOfNodes; i++) visitedInts.Add(null);

                queue.Enqueue(start);



                while (queue.Count > 0)
                {
                    int dequeuedNode = queue.Dequeue();

                    if (dequeuedNode == end)
                    {
                        break;
                    }

                    foreach (int node in AdjacementNodes(dequeuedNode))
                    {
                        if (!closedNodes.Contains(node))
                        {
                            queue.Enqueue(node);
                            closedNodes.Add(node);
                            visitedInts[node] = dequeuedNode;
                        }
                    }
                }

                List<int> output = new List<int>();
                int currentNode = end;

                while (currentNode != start)
                {
                    output.Add(currentNode);
                    if(visitedInts[currentNode] == null)
                    {
                        output = null;
                        break;
                    }
                    currentNode = (int)visitedInts[currentNode];
                }


                return output;

            }
        }
    }
}
