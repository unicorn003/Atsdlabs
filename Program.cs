using System;

namespace ConsoleApp1
{
    public class BinarySearchTree
    {
        protected Node Root;

        public class Node
        {
            public int Data;
            public Node Left, Right;

            public Node(int d)
            {
                Data = d;
            }
        }

        private Node recursiveAdd(Node current, int item)
        {
            if (current == null)
            {
                return new Node(item);
            }

            if (item.CompareTo(current.Data) < 0)
            {
                current.Left = recursiveAdd(current.Left, item);
            }
            else if (item.CompareTo(current.Data) > 0)
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

        public void AddItem(int item)
        {
            Root = recursiveAdd(Root, item);
        }

        private int FindSmallestValue(Node node)
        {
            return node.Left == null ? node.Data : FindSmallestValue(node.Left);
        }

        /**
     * Find node to delete, then delete it
     */
        private Node RecursiveDelete(Node current, int item)
        {
            if (current == null) return null;

            // Node to delete found
            if (item.Equals(current.Data))
            {
                // Node is a leaf
                if (current.Left == null && current.Right == null)
                {
                    return null;
                }

                // Node has only right child
                if (current.Left == null)
                {
                    return current.Right;
                }

                // Node has only left child
                if (current.Right == null)
                {
                    return current.Left;
                }

                // Node has both left and right children
                int smallestValue = FindSmallestValue(current.Right);
                current.Data = smallestValue;
                current.Right = RecursiveDelete(current.Right, smallestValue);
                return current;
            }

            if (item.CompareTo(current.Data) < 0)
            {
                current.Left = RecursiveDelete(current.Left, item);
                return current;
            }
            current.Right = RecursiveDelete(current.Right, item);
            return current;
        }

        public void DeleteItem(int item)
        {
            Root = RecursiveDelete(Root, item);
        }

        private void PrintTreePreorderRecursive(Node node)
        {
            if (node == null) return;

            Console.Write(node.Data + " ");
            PrintTreePreorderRecursive(node.Left);
            PrintTreePreorderRecursive(node.Right);
        }

        public void PrintTreePreorder()
        {
            PrintTreePreorderRecursive(this.Root);
            Console.Write("\n");
        }

        public static void Main()
        {
            BinarySearchTree t = new BinarySearchTree();
            t.AddItem(8);
            t.AddItem(3);
            t.AddItem(10);
            t.AddItem(1);
            t.AddItem(6);
            t.AddItem(14);
            t.AddItem(4);
            t.AddItem(7);
            t.AddItem(13);

            t.PrintTreePreorder();

            Console.WriteLine("delete 13");
            t.DeleteItem(13);
            t.PrintTreePreorder();
        }
    }
}
