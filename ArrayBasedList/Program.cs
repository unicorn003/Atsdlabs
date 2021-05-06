using System;

namespace ArrayBasedList
{
    using System;

    namespace Lab3
    {
        public class ArrayBasedList
        {
            private int[] ListArray;
            private static int DEFAULT_SIZE = 10;
            private int MaxSize;
            private int ListSize;

            ArrayBasedList(int size)
            {
                MaxSize = size;
                ListSize = 0;
                ListArray = new int[size];
            }

            ArrayBasedList() 
            {
                MaxSize = DEFAULT_SIZE;
                ListSize = 0;
                ListArray = new int[DEFAULT_SIZE];
            }

            public void Append(int el)
            {
                if (ListSize < MaxSize)
                    ListArray[ListSize++] = el;
            }

            public bool IsEmpty() { return ListSize == 0; }
            public bool IsFull() { return ListSize == MaxSize; }
            public int Size() { return ListSize; }

            public void PrintList()
            {
                foreach (int i in ListArray)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }

            public void HeapSort()
            {
                int temp;
                int size = ListSize - 1;

                for (int i = (ListSize / 2); i >= 0; i--)
                {
                    Heapify(i, size);
                }

                for (int i = size; i >= 0; i--)
                {
                    temp = ListArray[0];
                    ListArray[0] = ListArray[size];
                    ListArray[size] = temp;
                    size--;
                    Heapify(0, size);
                }
            }

            public void Heapify(int i, int HeapSize)
            {
                int a = 2 * i;
                int b = 2 * i + 1;
                int LargestElement;

                if (a <= HeapSize && ListArray[a] > ListArray[i])
                {
                    LargestElement = a;
                }
                else
                {
                    LargestElement = i;
                }
                if (b <= HeapSize && ListArray[b] > ListArray[LargestElement])
                {
                    LargestElement = b;
                }

                if (LargestElement != i)
                {
                    int temp = ListArray[i];
                    ListArray[i] = ListArray[LargestElement];
                    ListArray[LargestElement] = temp;
                    Heapify(LargestElement, HeapSize);
                }
            }
            public static int[] BuildHeap(params int[] arr)
            {
                int temp;
                int size = arr.Length - 1;

                for (int i = (arr.Length / 2); i >= 0; i--)
                {
                    HeapifyStatic(arr, i, size);
                }

                for (int i = size; i >= 0; i--)
                {
                    temp = arr[0];
                    arr[0] = arr[size];
                    arr[size] = temp;
                    size--;
                    HeapifyStatic(arr, 0, size);
                }

                return arr;
            }

            private static void HeapifyStatic(int[] arr, int i, int HeapSize)
            {
                int a = 2 * i;
                int b = 2 * i + 1;
                int LargestElement;

                if (a <= HeapSize && arr[a] > arr[i])
                {
                    LargestElement = a;
                }
                else
                {
                    LargestElement = i;
                }
                if (b <= HeapSize && arr[b] > arr[LargestElement])
                {
                    LargestElement = b;
                }

                if (LargestElement != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[LargestElement];
                    arr[LargestElement] = temp;
                    HeapifyStatic(arr, LargestElement, HeapSize);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
