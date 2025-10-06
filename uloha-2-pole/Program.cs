namespace uloha_2_pole
{
  internal class Program 
  {
    static int? FindMax(int[] inputArr) 
    {
      if (inputArr.Length == 0) return null;
      
      int max = inputArr[0];
      
      foreach (int i in inputArr)
      {
         if (i > max)
         {
             max = i;
        }
      }

      return max;
    }

    static int[] BubbleSort(int[] inputArr)
    {
      int[] arrToSort = (int[])inputArr.Clone();

      bool notDone = true;
      int a, b;

      while (notDone) 
      {
        notDone = false;
        for (int i = 0; i < arrToSort.Length-1; i++)
        {
           a = arrToSort[i];  
           b = arrToSort[i + 1];
           
           if (a > b) 
           {
              arrToSort[i] = b;
              arrToSort[i + 1] = a;
              notDone = true;
           }
        }
      }

      return arrToSort;
    }

    static int BinarySearch(int[] inputArr, int goal) 
    {
      int min = 0;
      int max = inputArr.Length-1;
      int mid;

      while (min <= max)
      {
        mid = min + ((max - min) / 2);

        if (goal == inputArr[mid]) 
        {
          return mid;
        }
        else if (goal < inputArr[mid]) 
        {
          max = mid - 1;
        }
        else if (goal > inputArr[mid]) 
        {
          min = mid + 1;
        }
      }
      return -1;
    } 



    static void Main(string[] args)
    {
      int[] sample = {1, 4, 6, 3, 0};

      Console.WriteLine(FindMax(sample));
      Console.WriteLine();
      foreach(var i in BubbleSort(sample))
      {
        Console.WriteLine(i.ToString());
      }
      Console.WriteLine();
      Console.WriteLine(BinarySearch(BubbleSort(sample), 5));
    }
  }
}
