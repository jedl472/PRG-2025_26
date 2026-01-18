namespace bonus_fronta_zasobnik
{
    internal class Program
    {
        static void Main(string[] args)
        {

            String inputString = Console.ReadLine();
            Console.WriteLine(Uzavorkovani(inputString));

            int input = Int32.Parse(Console.ReadLine());
            RozkladSouctu(input);

        }

        static Dictionary<char, int> Zavorky = new Dictionary<char, int>
        {
            { '(', 1 },
            { '{', 2 },
            { '[', 3 },
            { ')', -1 },
            { '}', -2 },
            { ']', -3 }
        };

        static bool Uzavorkovani(String zavorky)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in zavorky)
            {
                if (Zavorky[c] > 0)
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count > 0) 
                    {
                        if (Zavorky[stack.Pop()] != -Zavorky[c])
                        {
                            return false;
                        }
                    } else
                    {
                        return false;
                    }
                } 
            }

            if (stack.Count > 0) { return false; }
            return true;
        }

        static void RozkladSouctu(int n)
        {
            Stack<(List<int> kombinace, int zbyva)> stack = new Stack<(List<int>, int)>();
            stack.Push((new List<int>(), n));

            while (stack.Count > 0)
            {
                var (kombinace, zbyva) = stack.Pop();

                if (zbyva == 0)
                {
                    Console.WriteLine(string.Join(" + ", kombinace));
                }
                else
                {
                    int start = kombinace.Count > 0 ? kombinace[kombinace.Count - 1] : 1;

                    for (int i = start; i <= zbyva; i++)
                    {
                        List<int> novaKombinace = new List<int>(kombinace);
                        novaKombinace.Add(i);
                        stack.Push((novaKombinace, zbyva - i));
                    }
                }
            }
        }
    }
}
