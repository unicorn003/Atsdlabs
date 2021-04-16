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
            public int height;
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
            
            // Balancing
            root = rebalance(root);
        }
        
        private int height(Node node) {
            return node == null ? -1 : node.height;
        }
        
        private void updateHeight(Node current) {
            current.height = 1 + Math.Max(height(current.left), height(current.right));
        }
        
        private int getBalance(Node node) {
            return node == null ? 0 : height(node.right) - height(node.left);
        }
        
        private Node rotateLeft(Node y) {
            Node x = y.right;
            Node z = x.left;
            x.left = y;
            y.right = z;
            updateHeight(y);
            updateHeight(x);
            return x;
        }

        private Node rotateRight(Node y) {
            Node x = y.left;
            Node z = x.left;
            x.right = y;
            y.left = z;
            updateHeight(y);
            updateHeight(x);
            return x;
        }

        private Node rebalance(Node z) {
            updateHeight(z);
            int balance = getBalance(z);
            if (balance > 1) {
                if (height(z.right.right) <= height(z.right.left)) {
                    z.right = rotateRight(z.right);
                }
                z = rotateLeft(z);
            } else if (balance < -1) {
                if (height(z.left.left) < height(z.left.right)) {
                    z.left = rotateLeft(z.left);
                }
                z = rotateRight(z);
            }
            return z;
        }
    }
}