using System;
using System.Collections;

namespace Lab1
{
    public class BalancedBinarySearchTree
    {
        private Node _root;

        class Node
        {
            public int Data;
            public int Height;
            public Node Left, Right;

            public Node(int d)
            {
                Data = d;
            }
        }

        bool IsEmpty()
        {
            return _root == null;
        }

        bool IsFull()
        {
            return !IsEmpty();
        }

        private int recursiveSize(Node current)
        {
            if (current == null) return 0;

            return 1 + recursiveSize(current.Left) + recursiveSize(current.Right);
        }

        int Size()
        {
            return recursiveSize(_root);
        }


        private Node recursiveAdd(Node current, int item)
        {
            if (current == null)
            {
                return new Node(item);
            }

            if (item < current.Data)
            {
                current.Left = recursiveAdd(current.Left, item);
            } else if (item > current.Data)
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

        void AddItem(int item)
        {
            // Normal insert
            _root = recursiveAdd(_root, item);
            
            // Balancing
            _root = Rebalance(_root);
        }
        
        private int Height(Node node) {
            return node == null ? -1 : node.Height;
        }

        private void UpdateHeight(Node current) {
            current.Height = 1 + Math.Max(Height(current.Left), Height(current.Right));
        }
        
        private int GetBalance(Node node) {
            return node == null ? 0 : Height(node.Right) - Height(node.Left);
        }
        
        private Node RotateLeft(Node y) {
            Node x = y.Right;
            Node z = x.Left;
            x.Left = y;
            y.Right = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node RotateRight(Node y) {
            Node x = y.Left;
            Node z = x.Left;
            x.Right = y;
            y.Left = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node Rebalance(Node z) {
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
        
        private int FindSmallestValue(Node node)  {
            return node.Left == null ? node.Data : FindSmallestValue(node.Left);
        }
        
        /**
     * Find node to delete, then delete it
     */
        private Node RecursiveDelete(Node current, int item) {
            if (current == null) return null;

            // Node to delete found
            if (item == current.Data) {
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
                int smallestValue = FindSmallestValue(current.Right);
                current.Data = smallestValue;
                current.Right = RecursiveDelete(current.Right, smallestValue);
                return current;
            }

            if (item < current.Data) {
                current.Left = RecursiveDelete(current.Left, item);
                return current;
            }
            current.Right = RecursiveDelete(current.Right, item);
            return current;
        }

        void DeleteItem(int item) {
            _root = RecursiveDelete(_root, item);
        }
        
        private bool RecursiveSearch(Node current, int item) {
            if(current == null) {
                return false;
            }
            if (item == current.Data) {
                return true;
            }
            return item < current.Data
                ? RecursiveSearch(current.Left, item)
                : RecursiveSearch(current.Right, item);
        }

        bool Search(int item) {
            return RecursiveSearch(_root, item);
        }
        
        private void PrintTreePreorderRecursive(Node node) {
            if(node == null) return;

            Console.Write(node.Data + " ");
            PrintTreePreorderRecursive(node.Left);
            PrintTreePreorderRecursive(node.Right);
        }

        public void PrintTreePreorder() {
            PrintTreePreorderRecursive(this._root);
            Console.Write("\n");
        }

        private void PrintTreeInorderRecursive(Node node) {
            if(node == null) return;

            PrintTreeInorderRecursive(node.Left);
            Console.Write(node.Data + " ");
            PrintTreeInorderRecursive(node.Right);
        }

        void PrintTreeInorder() {
            PrintTreeInorderRecursive(this._root);
            Console.Write("\n");
        }

        private void PrintTreePostorderRecursive(Node node) {
            if(node == null) return;

            PrintTreePostorderRecursive(node.Left);
            PrintTreePostorderRecursive(node.Right);
            Console.Write(node.Data + " ");
        }

        void PrintTreePostorder() {
            PrintTreePostorderRecursive(this._root);
            Console.Write("\n");
        }
        
        public int GetParent(int value) {
            Node current = _root;
            if (current == null || (int) current.Data == value) {
                return -10000;
            }

            while (true) {
                if (current.Left == null && current.Right == null)
                    return -10000;
                if (current.Left != null && (int) current.Left.Data == value ||
                    current.Right != null && (int) current.Right.Data == value)
                    return (int) current.Data;

                if ((int) current.Data > value)
                    current = current.Left;
                else current = current.Right;
            }
        }
        
        public static BalancedBinarySearchTree FromArray(params int[] arr) {
            BalancedBinarySearchTree tree = new BalancedBinarySearchTree();

            foreach(int i in arr) {
                tree.AddItem(i);
            }

            return tree;
        }
        
        public static void Main()
        {
            BalancedBinarySearchTree t = new BalancedBinarySearchTree();
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
            
            Console.WriteLine("getParent(8)");
            Console.WriteLine(t.GetParent(8));
            Console.WriteLine("getParent(14)");
            Console.WriteLine(t.GetParent(14));
            Console.WriteLine("getParent(666)");
            Console.WriteLine(t.GetParent(666));
        }
    }
}