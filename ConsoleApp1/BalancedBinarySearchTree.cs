using System;
using System.Collections.Generic;

namespace Lab1
{
    public class BalancedBinarySearchTree<T> where T : IComparable<T>
    {
        private Node<T> _root;
        private List<T> _elementsBuffer;

        class Node<TS>
        {
            public TS Data;
            public int Height;
            public Node<TS> Left, Right;

            public Node(TS d)
            {
                Data = d;
            }
        }

        public bool IsEmpty()
        {
            return _root == null;
        }

        public bool IsFull()
        {
            return !IsEmpty();
        }

        private int recursiveSize(Node<T> current)
        {
            if (current == null) return 0;

            return 1 + recursiveSize(current.Left) + recursiveSize(current.Right);
        }

        int Size()
        {
            return recursiveSize(_root);
        }


        private Node<T> recursiveAdd(Node<T> current, T item)
        {
            if (current == null)
            {
                return new Node<T>(item);
            }

            if (item.CompareTo(current.Data) < 0)
            {
                current.Left = recursiveAdd(current.Left, item);
            } else if (item.CompareTo(current.Data) > 0)
            {
                current.Right = recursiveAdd(current.Right, item);
            }
            else
            {
                //value already exists
                return current;
            }

            return current;
        }

        public void AddItem(T item)
        {
            // Normal insert
            _root = recursiveAdd(_root, item);
            
            // Balancing
            _root = Rebalance(_root);
        }
        private int Height(Node<T> node)
        {
            return node == null ? -1 : node.Height;
        }

        private void UpdateHeight(Node<T> current) {
            current.Height = 1 + Math.Max(Height(current.Left), Height(current.Right));
        }
        
        private int GetBalance(Node<T> node) {
            return node == null ? 0 : Height(node.Right) - Height(node.Left);
        }
        
        private Node<T> RotateLeft(Node<T> y) {
            Node<T> x = y.Right;
            Node<T> z = x.Left;
            x.Left = y;
            y.Right = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node<T> RotateRight(Node<T> y) {
            Node<T> x = y.Left;
            Node<T> z = x.Left;
            x.Right = y;
            y.Left = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node<T> Rebalance(Node<T> z) {
            UpdateHeight(z);
            int balance = GetBalance(z);
            if (balance > 1) {
                if (Height(z.Right.Right) <= Height(z.Right.Left)) {
                    z.Right = RotateRight(z.Right);
                }
                z = RotateLeft(z);
            } else if (balance < -1) {
                if (Height(z.Left.Left) < Height(z.Left.Right)) {
                    z.Left = RotateLeft(z.Left);
                }
                z = RotateRight(z);
            }
            return z;
        }
        
        private T FindSmallestValue(Node<T> node)  {
            return node.Left == null ? node.Data : FindSmallestValue(node.Left);
        }
        
        /**
     * Find node to delete, then delete it
     */
        private Node<T> RecursiveDelete(Node<T> current, T item) {
            if (current == null) return null;

            // Node to delete found
            if (item.Equals(current.Data)) {
                // Node is a leaf
                if (current.Left == null && current.Right == null) {
                    return null;
                }

                // Node has only right child
                if (current.Left == null) {
                    return current.Right;
                }

                // Node has only left child
                if (current.Right == null) {
                    return current.Left;
                }

                // Node has both left and right children
                T smallestValue = FindSmallestValue(current.Right);
                current.Data = smallestValue;
                current.Right = RecursiveDelete(current.Right, smallestValue);
                return current;
            }

            if (item.CompareTo(current.Data) < 0) {
                current.Left = RecursiveDelete(current.Left, item);
                return current;
            }
            current.Right = RecursiveDelete(current.Right, item);
            return current;
        }

        public void DeleteItem(T item) {
            _root = RecursiveDelete(_root, item);
        }
        
        private bool RecursiveSearch(Node<T> current, int item) {
            if(current == null) {
                return false;
            }
            if (item.Equals(current.Data)) {
                return true;
            }
            return item.CompareTo(current.Data) < 0
                ? RecursiveSearch(current.Left, item)
                : RecursiveSearch(current.Right, item);
        }

        public bool Search(int item) {
            return RecursiveSearch(_root, item);
        }
        
        private void PrintTreePreorderRecursive(Node<T> node) {
            if(node == null) return;

            Console.Write(node.Data + " ");
            PrintTreePreorderRecursive(node.Left);
            PrintTreePreorderRecursive(node.Right);
        }

        public void PrintTreePreorder() {
            PrintTreePreorderRecursive(this._root);
            Console.Write("\n");
        }

        private void PrintTreeInorderRecursive(Node<T> node) {
            if(node == null) return;

            PrintTreeInorderRecursive(node.Left);
            Console.Write(node.Data + " ");
            PrintTreeInorderRecursive(node.Right);
        }

        public void PrintTreeInorder() {
            PrintTreeInorderRecursive(this._root);
            Console.Write("\n");
        }

        private void PrintTreePostorderRecursive(Node<T> node) {
            if(node == null) return;

            PrintTreePostorderRecursive(node.Left);
            PrintTreePostorderRecursive(node.Right);
            Console.Write(node.Data + " ");
        }

        public void PrintTreePostorder() {
            PrintTreePostorderRecursive(this._root);
            Console.Write("\n");
        }

        private void FillElementsBuffer(Node<T> node)
        {
            if(node == null) return;
            _elementsBuffer.Add(node.Data);
            FillElementsBuffer(node.Left);
            FillElementsBuffer(node.Right);
        }

        /// It prints keys of a BBST in 2 lines:
        /// -  Sorted in ascending order (first line)
        /// -  Sorted in descending order (second line )
        public void PrintSorted()
        {
            _elementsBuffer = new List<T>();
            FillElementsBuffer(_root);
            
            // Array.Sort(_elementsBuffer);
            _elementsBuffer.Sort();
            foreach (T el in _elementsBuffer)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();

            for (int i = _elementsBuffer.Count -1; i >= 0; i--)
            {
                Console.Write(_elementsBuffer[i] + " ");
            }
            Console.WriteLine();
        }
        
        public T FatherNode(T value) {
            Node<T> current = _root;
            if (current == null || current.Data.Equals(value)) {
                throw new Exception("-10000");
            }

            while (true) {
                if (current.Left == null && current.Right == null)
                    throw new Exception("-10000");
                if (current.Left != null && current.Left.Data.Equals(value) ||
                    current.Right != null && current.Right.Data.Equals(value))
                    return current.Data;

                if (current.Data.CompareTo(value) > 0)
                    current = current.Left;
                else current = current.Right;
            }
        }
        
        public static BalancedBinarySearchTree<T> FromArray(params T[] arr) {
            BalancedBinarySearchTree<T> tree = new BalancedBinarySearchTree<T>();

            foreach(T i in arr) {
                tree.AddItem(i);
            }

            return tree;
        }
        
        public static void Main()
        {
            BalancedBinarySearchTree<int> t = new BalancedBinarySearchTree<int>();
            Console.WriteLine("isEmpty()");
            Console.WriteLine(t.IsEmpty());
            t.AddItem(8);
            t.AddItem(3);
            t.AddItem(10);
            t.AddItem(1);
            t.AddItem(6);
            t.AddItem(14);
            t.AddItem(4);
            t.AddItem(7);
            t.AddItem(13);
            Console.WriteLine(t.IsEmpty());
            
            Console.WriteLine();
            
            Console.WriteLine("Preorder");
            t.PrintTreePreorder();
            Console.WriteLine("Inorder");
            t.PrintTreeInorder();
            Console.WriteLine("Postorder");
            t.PrintTreePostorder();
            
            Console.WriteLine();
            
            Console.WriteLine("Search 3:");
            Console.WriteLine(t.Search(3));
            Console.WriteLine("Search 18:");
            Console.WriteLine(t.Search(18));
            
            Console.WriteLine();
            
            Console.WriteLine("delete 13");
            t.DeleteItem(13);
            t.PrintTreeInorder();

            Console.WriteLine();
            
            try
            {
                Console.WriteLine("FatherNode(8)");
                Console.WriteLine(t.FatherNode(8));
            }
            catch (Exception e)
            {
                Console.WriteLine("-10000");
            }

            Console.WriteLine("FatherNode(14)");
            Console.WriteLine(t.FatherNode(14));
            
            try
            {
                Console.WriteLine("FatherNode(666)");
                Console.WriteLine(t.FatherNode(666));
            }
            catch (Exception e)
            {
                Console.WriteLine("-10000");
            }
        
            Console.WriteLine();
            
            t.PrintSorted();
        }
    }
}