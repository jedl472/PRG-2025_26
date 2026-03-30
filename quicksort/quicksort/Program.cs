using static System.Net.Mime.MediaTypeNames;

/*  BONUS 1: Algoritmus "Medián mediánů"
 *  pro vstup P
 *  
 *  1. Rozdělíme P na stejné kusy o n prvcích (n vybereme tak malé, aby na těchto kusech bylo triviální seřadit je, používá se n=5)
 *  2. Každý z kusů seřadíme (protože n je konst., časová složitost řazení je linearní), najdeme na něm medián 
 *  3. Vezmeme všechny takto nalezené mediány, a uděláme z nich nové P, následně algoritmus opakujeme dokud není velikost P <= n
 */

/*  BONUS 4: {1, 2, 100000000000} 
 *  medián = 2
 *  cca průměr = 33,333,333,334
 */

namespace quicksort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] test = { 8, 3, 1, 7, 0, 10, 2 };

            Console.WriteLine(string.Join(" ", test));

            QuickSortClass.QuickSort(test);

            Console.WriteLine(string.Join(" ", test));

        }


        class QuickSortClass
        {
            private static int FindPivot(int[] array, int l, int r) // nalezne skoromedian ze 3 prvku
            {
                int a = array[l];
                int b = array[l + (r - l) / 2];
                int c = array[r];

                if (a < b) {
                    if (b > c) return b;
                    return a > c ? c : a;
                } else
                {
                    if (a > c) return a;
                    return b > c ? c : b;
                }
            }

            public static void QuickSort(int[] arrayToSort)
            {
                void _quicksort(int[] array, int l, int r)
                {
                    if (l >= r) return;

                    int p = FindPivot(array, l, r);

                    int i = l;
                    int j = r;

                    while (i <= j)
                    {                        
                        while (array[i] < p) i++;
                        while (array[j] > p) j--;

                        if (i < j)
                        {
                            int c = array[i]; // prohození P[i] a P[j] - c je pomocná proměnná
                            array[i] = array[j];
                            array[j] = c;
                        }

                        if (i <= j) 
                        {
                            i++;
                            j--;
                        }
                    }
                    _quicksort(array, l, j);
                    _quicksort(array, i, r);
                }


                if (arrayToSort != null && arrayToSort.Length > 1)
                {
                    _quicksort(arrayToSort, 0, arrayToSort.Length - 1);
                }
            }
        }
    }
}
