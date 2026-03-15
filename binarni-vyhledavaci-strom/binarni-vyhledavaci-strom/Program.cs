using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace binarniVyhledavaciStromy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("../../../../studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }

            // Najděte studenta s ID 20 (David Urban (ID: 20) ze třídy 4.A)
            Console.WriteLine(tree.FindNodeByKey(20).Value.ToString());

            // Najděte studenta s nejnižším ID (Kateřina Sedláček (ID: 1) ze třídy 1.B)
            Console.WriteLine(tree.MinValue());

            // Vložte vlastního studenta s ID > 100 (je potřeba vytvořit nový objekt typu Student) a zkuste ho pak najít
            Student newStudent = new Student(001, "Aaron", "Siegel", 155, "O5");
            tree.Insert(newStudent.Id, newStudent);
            Console.WriteLine(tree.FindNodeByKey(newStudent.Id).Value.ToString());

            // Smažte všechny studenty se sudým ID
            foreach(var node in tree.Collapse())
            {
                if (node.Key % 2 == 0)
                {
                    tree.Remove(node.Key);
                }
                
            }
            // Vypište strom (měli byste vidět jen ID lichá a seřazená)
            tree.Print();





        }
    }

    class BinarySearchTree<T>
    {
        public Node<T> Root;

        public void Insert(int newKey, T newValue)
        {

            void _insert(Node<T> node, int newKey, T newValue)
            {                
                if (newKey < node.Key) // jdeme doleva
                    if(node.LeftSon== null)
                        node.LeftSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.LeftSon, newKey, newValue);
                else if (newKey > node.Key) // jdeme doprava
                    if (node.RightSon == null)
                        node.RightSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.RightSon, newKey, newValue);
                else // našli jsme náš klíč, což bychom neměli, mají být unikátní.... :/
                    throw new Exception("Key already found in tree. "); // vyhodíme chybu
            }

            if(Root == null) // pokud ještě není definován kořen
                Root = new Node<T>(newKey, newValue);
            else
                _insert(Root, newKey, newValue);
        }

        public void Print()
        {
            void _print(Node<T> node, string indent, bool last)
            {
                if (node == null) return;

                Console.Write(indent);

                if (last)
                {
                    Console.Write("└── ");
                    indent += "    ";
                }
                else
                {
                    Console.Write("├── ");
                    indent += "│   ";
                }

                Console.WriteLine(node.Key);

                bool hasLeft = node.LeftSon != null;
                bool hasRight = node.RightSon != null;

                if (hasLeft || hasRight)
                {
                    _print(node.LeftSon, indent, !hasRight);
                    _print(node.RightSon, indent, true);
                }
            }
            
            _print(Root, "", true);
        }

        public void Remove(int keyToRemove)
        {
            var pathToNode = FindNodeByKeyBacktrack(keyToRemove);
            bool isRoot = false;
            if (keyToRemove == Root.Key) isRoot = true;

            //Console.WriteLine(keyToRemove);

            // Node který máme odstranit nemá děti
            if(pathToNode.Last().LeftSon == null && pathToNode.Last().RightSon == null)
            {
                if (isRoot)
                    Root = null;

                else if (pathToNode[^2].Key > pathToNode.Last().Key) 
                    pathToNode[^2].LeftSon = null;

                else if (pathToNode[^2].Key < pathToNode.Last().Key)
                    pathToNode[^2].RightSon = null;
            }

            // Node který máme odstranit nemá pravého syna
            else if (pathToNode.Last().LeftSon == null)
            {
                if (isRoot) 
                    Root = pathToNode.Last().RightSon;
                else if (pathToNode[^2].Key > pathToNode.Last().Key)
                    pathToNode[^2].LeftSon = pathToNode.Last().RightSon;

                else if (pathToNode[^2].Key < pathToNode.Last().Key)
                    pathToNode[^2].RightSon = pathToNode.Last().RightSon;
            }

            // Node který máme odstranit nemá levého syna
            else if (pathToNode.Last().RightSon == null)
            {
                if (isRoot)
                    Root = pathToNode.Last().LeftSon;
                else if (pathToNode[^2].Key > pathToNode.Last().Key)
                    pathToNode[^2].LeftSon = pathToNode.Last().LeftSon;

                else if (pathToNode[^2].Key < pathToNode.Last().Key)
                    pathToNode[^2].RightSon = pathToNode.Last().LeftSon;
            }

            // Node který máme odstranit má oba syny
            else
            {
                Node<T> minNode = Min(pathToNode.Last().RightSon);

                Remove(minNode.Key);

                minNode.RightSon = pathToNode.Last().RightSon;
                minNode.LeftSon = pathToNode.Last().LeftSon;


                if (isRoot)
                {
                    Root = minNode;
                }

                else if (pathToNode[^2].Key > pathToNode.Last().Key)
                {
                    pathToNode[^2].LeftSon = minNode;

                }
                    

                else if (pathToNode[^2].Key < pathToNode.Last().Key)
                {
                    pathToNode[^2].RightSon = minNode;
                }
            }
        }

        public Node<T> FindNodeByKey(int keyToFind)
        {            
            Node<T> currentNode = Root;

            while (currentNode != null)
            {
                if (keyToFind == currentNode.Key)
                {
                    return currentNode;
                }

                if (keyToFind < currentNode.Key)
                    currentNode = currentNode.LeftSon;
                else
                    currentNode = currentNode.RightSon;
            }

            throw new Exception("FindValueByKey was unable to find the key.");

        }

        public List<Node<T>> FindNodeByKeyBacktrack(int keyToFind)
        {
            List<Node<T>> output = new List<Node<T>>();
            
            output.Add(Root);

            while (output.Last() != null)
            {
                if (keyToFind == output.Last().Key)
                {
                    return output;
                }

                if (keyToFind < output.Last().Key)
                    output.Add(output.Last().LeftSon);
                else
                    output.Add(output.Last().RightSon);
            }

            throw new Exception("FindValueByKey was unable to find the key.");
        }

        public static Node<T> Min(Node<T> rootNode)
        {
            Node<T> _min(Node<T> node)
            {
                if (node.LeftSon == null)
                {
                    return node;
                }

                return _min(node.LeftSon);
            }

            if (rootNode == null)
                throw new Exception("Using Min() on an empty tree.");

            return _min(rootNode);

        }

        public T MinValue()
        {
            return Min(Root).Value;
        }

        public static Node<T> Max(Node<T> rootNode)
        {
            Node<T> _min(Node<T> node)
            {
                if (node.RightSon == null)
                {
                    return node;
                }

                return _min(node.RightSon);
            }

            if (rootNode == null)
                throw new Exception("Using Max() on an empty tree.");

            return _min(rootNode);

        }

        public T MaxValue()
        {
            return Max(Root).Value;
        }

        public static int Depth(Node<T> rootNode) 
        {
            int _depth(Node<T> node)
            {
                if (node == null)
                    return 0;

                int leftDepth = _depth(node.LeftSon);
                int rightDepth = _depth(node.RightSon);

                return 1 + Math.Max(leftDepth, rightDepth);
            }

            if (rootNode == null)
                return 0;
            else
                return _depth(rootNode);
        }

        //Odstřihne ze stromu všechny větve a nahází je na hromadu (do listu)
        public List<Node<T>> Collapse()
        {
            var result = new List<Node<T>>();
            if (Root == null) return result;

            var queue = new Queue<Node<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node);

                if (node.LeftSon != null)
                    queue.Enqueue(node.LeftSon);

                if (node.RightSon != null)
                    queue.Enqueue(node.RightSon);
            }

            return result;
        }
    }

    class Node<T> // T může být libovolný typ
    {
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
        public int Key;
        public T Value;

        public Node<T> LeftSon;
        public Node<T> RightSon;


        
    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }

}