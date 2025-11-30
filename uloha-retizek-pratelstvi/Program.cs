using System;

namespace graph_utility {
    internal class Program {
        class Node {
            public int value;
            public int uloha_db;
            public int distanceFromOrigin;

            public Node? predecessor;
        }

        class Graph {
            public Dictionary<int, List<int>> edges = new Dictionary<int, List<int>>(); // list of node values connected by 
            public List<Node> adjacement_nodes(Node origin_node) { 
                List<Node> out_list = new List<Node>();

                foreach (var val in edges[origin_node.value]) {
                    out_list.Add(new Node(){ value = val, predecessor = origin_node});
                }
                return out_list; 
            }
        }

        class LIFO {
            public List<Node> queue = new List<Node>();

            public void add(Node node) {
                queue.Add(node);
            }
            public Node removeElement() { 
                Node node = queue[0];
                queue.RemoveAt(0);

                return node; 
            }

            public bool isQueueEmpty() {
                if (queue.Count() == 0) { 
                    return true; 
                } else { 
                    return false; 
                }
            }
        }

        class BFS {
            public Graph graph;
            List<int> explored = new List<int>();
            LIFO queue = new LIFO(){};
            public int target_value;

            public Node? step(out bool no_result) {
                no_result = false;
                if(queue.isQueueEmpty()) { 
                    no_result = true; 
                    return null; 
                }

                Node origin_node = queue.removeElement();
                // Console.Write($"origin: {origin_node.value}");
                foreach (Node node in graph.adjacement_nodes(origin_node))
                {
                    // Console.Write("searching: "); Console.Write($"{node.value}, ");
                    if(node.value == target_value) {
                        return node;
                    } else {
                        if(!explored.Contains(node.value)) queue.add(node);
                    }
                }

                explored.Add(origin_node.value);

                // Console.WriteLine();
                return null;
            }

            public List<int> backtrack(Node node) {
                List<int> out_list = new List<int>() { node.value };
                Node i_node = node;

                while (i_node.predecessor != null) {
                    out_list.Add(i_node.predecessor.value);
                    i_node = i_node.predecessor;
                }
                return out_list;
            }

            public void init_queue(Node node) {
                queue.queue.Add(node);
            }
        }


        static void Main(string[] args) {
            Graph graph = new Graph();



            int pocetUzivatelu = Int32.Parse(Console.ReadLine());
            string hrany = Console.ReadLine();
            string start_cil_raw = Console.ReadLine();
            int[] start_cil = new int[] { Int32.Parse(start_cil_raw.Split(' ')[0]), Int32.Parse(start_cil_raw.Split(' ')[1]) };

            for (int i = 1; i <= pocetUzivatelu; i++)
            {
                graph.edges.Add(i, new List<int> {}); 
            }

            foreach(string hrana in hrany.Split(' ')) {
                int[] dvojice = new int[] { Int32.Parse(hrana.Split('-')[0]), Int32.Parse(hrana.Split('-')[1]) };

                graph.edges[dvojice[0]].Add(dvojice[1]);
                graph.edges[dvojice[1]].Add(dvojice[0]);

            }


            // printing out the graph dict
            // foreach (var kvp in graph.edges)
            // {
            //   Console.WriteLine($"{kvp.Key}: {string.Join(", ", kvp.Value)}");
            // }

            bool no_result = false;
            Node bfs_result = null;
            BFS bfs = new BFS() { target_value = start_cil[1], graph = graph };
            bfs.init_queue(new Node() { value = start_cil[0] });
            while (bfs_result == null && (!no_result)) {
                bfs_result = bfs.step(out no_result);
            }

            Console.WriteLine();
            if (no_result == true) { Console.Write("neexistuje"); }
            else {
                var bfs_backtrack = bfs.backtrack(bfs_result);

                for (int i = bfs_backtrack.Count - 1; i >= 0; i--)
                {
                    Console.Write($"{bfs_backtrack[i]} ");
                }
                Console.WriteLine();
            }
        }

    }
}
