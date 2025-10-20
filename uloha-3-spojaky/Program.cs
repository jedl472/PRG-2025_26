using System;

namespace Spojak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList spojak = new LinkedList();
            spojak.AddToEnd(7);
            spojak.AddToEnd(4);
            spojak.AddToEnd(4);
            spojak.AddToEnd(3);
            spojak.AddToEnd(3);
            /*spojak.AddToEnd(4);
            spojak.AddToEnd(5);
            spojak.AddToEnd(6);
            spojak.AddToEnd(3);*/

            LinkedList spojak2 = new LinkedList();
            spojak2.AddToEnd(4);
            spojak2.AddToEnd(4);
            /*spojak2.AddToEnd(3);
            spojak2.AddToEnd(2);
            spojak2.AddToEnd(13);*/

            Console.Write("Spojak 1: ");
            spojak.Print();
            Console.Write("Spojak 2: ");
            spojak2.Print();
            
            Console.Write("Intersection: ");
            Intersection(spojak, spojak2).Print();
            
            Console.Write("Union: ");
            Union(spojak, spojak2).Print();

            Console.WriteLine($"Sum: {AddLinkedLists(spojak, spojak2)}");
        }

        static LinkedList Intersection(LinkedList list1, LinkedList list2) // časová složitost = O(N) 
        {
            const int HASHMAPLEN = 10;

            LinkedList[] list1_hashMap = new LinkedList[HASHMAPLEN];
            for (int i = 0; i < list1_hashMap.Length; i++) list1_hashMap[i] = new LinkedList();
            LinkedList outputList = new LinkedList();

            // Výroba hash mapy
            Node node = list1.Head;
            while (node != null)
            {
                int hashValue = node.Value % HASHMAPLEN; 
                if (!list1_hashMap[hashValue].Exists(node.Value))
                { 
                    list1_hashMap[hashValue].AddToEnd(node.Value);
                }

                node = node.Next;
            }

            // procházení hash mapy a porovnávání
            node = list2.Head;
            while (node != null)
            {
                int hashValue = node.Value % HASHMAPLEN; 
                if(list1_hashMap[hashValue].Exists(node.Value)) 
                {
                    list1_hashMap[hashValue].RemoveAll(node.Value);
                    outputList.AddToEnd(node.Value);
                } 
                node = node.Next;
            }

            return outputList;
        }

        static LinkedList Union(LinkedList list1, LinkedList list2) // časová složitost = O(N) 
       {
            const int HASHMAPLEN = 10;

            LinkedList[] hashMap = new LinkedList[HASHMAPLEN];
            for (int i = 0; i < hashMap.Length; i++) hashMap[i] = new LinkedList();
            LinkedList outputList = new LinkedList();

            // Procházení hash mapy pro list 1
            Node node = list1.Head;
            while (node != null)
            {
                int hashValue = node.Value % HASHMAPLEN; 
                if (!hashMap[hashValue].Exists(node.Value))
                { 
                    hashMap[hashValue].AddToEnd(node.Value);
                    outputList.AddToEnd(node.Value);

                }

                node = node.Next;
            }

            // Procházení hash mapy pro list 2
            node = list2.Head;
            while (node != null)
            {
                int hashValue = node.Value % HASHMAPLEN; 
                if (!hashMap[hashValue].Exists(node.Value))
                { 
                    hashMap[hashValue].AddToEnd(node.Value);
                    outputList.AddToEnd(node.Value);

                }

                node = node.Next;
            }

            return outputList;
        }
        
        //Tato funkce počítá s tím že jsou ve vstupních spojácích číslice ve formátu nejméně signifikantní čislice jako hlavy spojáku
        static int? AddLinkedLists(LinkedList list1, LinkedList list2) // časová složitost = O(N) 
        {
            const int DIGITLIMIT = 10; 

            Node node1 = list1.Head; Node node2 = list2.Head;
            LinkedList output = new LinkedList();
            
            // sečtení a uložení do spojáku
            if(node1 != null || node2 != null) 
            {
                int carry = 0;
                while (!(node1 == null && node2 == null) || carry != 0) 
                {
                    int digit = (node1 != null ? node1.Value : 0) + (node2 != null ? node2.Value : 0) + carry;
                    if(digit >= DIGITLIMIT) 
                    {
                        carry = (digit/DIGITLIMIT);
                        output.AddToEnd(digit % DIGITLIMIT);
                    } else {
                        output.AddToEnd(digit);
                        carry = 0;
                    }

                    if (node1 != null) node1 = node1.Next;
                    if (node2 != null) node2 = node2.Next;
                }
            }

            // převedení spojáku na int
            Node node = output.Head;
            int orderOfMagnitude = 1;
            int outputInt = 0;

            while (node != null)
            {
                outputInt += (orderOfMagnitude * node.Value);
                orderOfMagnitude *= DIGITLIMIT;
                
                node = node.Next;
            }
            return outputInt;
        }
    }

    class Node
    {
        // konstruktor
        public Node(int value) 
        { 
            Value = value;
            Next = null; 
        }
        public int Value { get; set; }
        public Node? Next { get; set; }  // nejsem si uplně jistý, proč je zde potřeba specifikovat ? pokud je object defaultne nullable, ale compiler bez toho hází warning
    }

    class LinkedList
    {
        public Node? Head { get; set; }
        public void AddToEnd(int value)
        {
            if(Head == null)
            {
                Head = new Node(value);
            }
            else
            {
                Node currentNode = Head;
                while(currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = new Node(value);
            }
        }

        public void Print()
        {
            Node node = Head;
            while (node != null)
            {
                Console.Write(node.Value);
                Console.Write(" ");
                node = node.Next;
            }
            Console.WriteLine();

        }

        public int? FindMax() //časová složitost O(N)
            // int s otazníkem znamená nullovatelný int - může obsahovat číslo i null 
        {
            if (Head == null)
            {
                return null; // nullem naznačíme, že maximum nebylo nalezeno
            }
            else
            {
                Node node = Head;
                int x = node.Value;
                while (node != null)
                {
                    if (node.Value > x)
                    {
                        x = node.Value;
                    }
                    node = node.Next;
                }
                return x;
            }

        }

        public void RemoveFromEnd() //časová složitost O(n)
        {
            if (Head != null) 
            {   
                if(Head.Next == null) 
                {
                    Head = null;
                } else {
                    Node node = Head;
                    
                    while (node.Next.Next != null) // compiler warning, ale podmínky před tím kontrolují jestli se nejedná o null type dereference
                    {
                        node = node.Next;
                    }
                    node.Next = null;
                }   
            }
        }
        
        public bool Exists(int valueToFind) //časová složitost O(N)
        {
            Node node = Head;
            while (node != null)
            {
                if(node.Value == valueToFind)
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public void RemoveAll(int valueToFind) //časová složitost O(N)
        {
            while (Head != null && Head.Value == valueToFind)
            {
                Head = Head.Next;
            }

            Node node = Head;

            while (node != null && node.Next != null)

            {
                if (node.Next.Value == valueToFind)
                {
                    node.Next = node.Next.Next;
                }
                else
                {
                    node = node.Next; 
                }
            }
        }
    }
}
