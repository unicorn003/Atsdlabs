using System;
using System.Collections;

namespace Lab1
{
    public class BalancedBinarySearchTree
    {
        private Node root;

        class Node
        {
            public int data;
            public Node left, right;

            public Node(int d)
            {
                data = d;
            }
        }

        bool isEmpty()
        {
            return root == null;
        }

        bool isFull()
        {
            return !isEmpty();
        }

        private int recursiveSize(Node current)
        {
            if (current == null) return 0;

            return 1 + recursiveSize(current.left) + recursiveSize(current.right);
        }

        int size()
        {
            return recursiveSize(root);
        }


        private Node recursiveAdd(Node current, int item)
        {
            if (current == null)
            {
                return new Node(item);
            }

            if (item < current.data)
            {
                current.left = recursiveAdd(current.left, item);
            } else if (item > current.data)
            {
                current.right = recursiveAdd(current.right, item);
            }
            else
            {
                //value already exists
                return current;
            }

            return current;
        }

        void addItem(int item)
        {
            // Normal insert
            root = recursiveAdd(root, item);
            
            // TODO rebalance
        }
    }
}