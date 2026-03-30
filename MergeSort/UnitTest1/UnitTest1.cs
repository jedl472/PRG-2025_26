using Microsoft.VisualStudio.TestPlatform.TestHost;
using mergesort;

namespace UnitTest1
{
    public class UnitTest1
    {
        [Fact]  // Tím ozna?ujeme, že jde o testovací metodu      
        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co pot?ebujeme, aby mohla b?žet testovaná funkce
            int[] array = { 1, 3, 5, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_LeftArraysSmaller_ReturnsMergedArray()
        {
            int[] array = { 1, 2, 10, 20 };
            int[] expectedArray = { 1, 2, 10, 20 };

            MergeSortClass.Merge(array, 0, 1, 3);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_RightElementsSmaller_ReturnsMergedArray()
        {
            int[] array = { 10, 20, 1, 2 };
            int[] expectedArray = { 1, 2, 10, 20 };

            MergeSortClass.Merge(array, 0, 1, 3);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_WithDuplicates_ReturnsMergedArray()
        {
            int[] array = { 3, 5, 3, 7 };
            int[] expectedArray = { 3, 3, 5, 7 };

            MergeSortClass.Merge(array, 0, 1, 3);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_SingleElements_ReturnsMergedArray()
        {
            int[] array = { 6, 3 };
            int[] expectedArray = { 3, 6 };

            MergeSortClass.Merge(array, 0, 0, 1);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_Null_ReturnsNull()
        {
            Assert.Throws<ArgumentNullException>(() => MergeSortClass.Merge(null, 0, 0, 1));
        }






        [Fact]
        public void MergeSort_RandomArray_ReturnsMergedSortedArray()
        {
            int[] array = { 38, 27, 43, 3, 9, 82, 10 };
            int[] expectedArray = { 3, 9, 10, 27, 38, 43, 82 };

            MergeSortClass.Sort(array);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void MergeSort_SortedArray_ReturnsMergedSortedArray()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int[] expectedArray = { 1, 2, 3, 4, 5 };

            MergeSortClass.Sort(array);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void MergeSort_ReverseSortedArray_ReturnsMergedSortedArray()
        {
            int[] array = { 5, 4, 3, 2, 1 };
            int[] expectedArray = { 1, 2, 3, 4, 5 };

            MergeSortClass.Sort(array);

            Assert.Equal(expectedArray, array);
        }
    }
}