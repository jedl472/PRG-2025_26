using System;

namespace Spojak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList spojak = new LinkedList();
            spojak.AddToEnd(3);
            spojak.AddToEnd(4);
            spojak.AddToEnd(5);
            spojak.AddToEnd(6);

            spojak.Print();
            
            spojak.RemoveAll(4);

            spojak.Print();

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

        public int? FindMax()
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

        public void RemoveFromEnd()
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
        
        public bool Exists(int valueToFind)
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

        public bool RemoveAll(int valueToFind)
        {
            Node node = Head;
            while (node != null)
            {
                if(node.Next.Value == valueToFind)
                {
                    node.Next = node.Next.Next;
                }
                node = node.Next;
            }
            return false;
        }
    }
}
